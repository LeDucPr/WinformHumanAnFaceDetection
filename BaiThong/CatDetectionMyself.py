import datetime
import numpy as np
import cv2
from keras.models import load_model
import tensorflow as tf

class FaceDct_CatModel:
    def __init__(self):
        self.cat_model = load_model("my_model_ImageNet_2_BackGroundGT.h5")
        self.net = cv2.dnn.readNetFromCaffe("deploy.prototxt", "mobilenet_iter_73000.caffemodel") # SSD + ResNet caffe model 300x300 
        self.cat_list = ["Abyssinian","American Bobtail","American Curl","American Shorthair","Bengal","Birman","Bombay","British Shorthair","Egyptian Mau","Exotic Shorthair","Maine Coon","Manx","Norwegian Forest","Persian","Ragdoll","Russian Blue","Scottish Fold","Siamese","Sphynx","Turkish Angora"]
        physical_devices = tf.config.experimental.list_physical_devices("GPU");
        if (len(physical_devices) > 0): 
            print (physical_devices) 
    def AcrFaceDct_Size_Cat(self, image):
        (height, width) = image.shape[:2]
        blob = cv2.dnn.blobFromImage(cv2.resize(image, (300, 300)), 0.007843, (300, 300), (127.5, 127.5, 127.5))
        self.net.setInput(blob) # cho thằng này vào dnn 
        detections = self.net.forward() # xác định đầu ra (cái này tương tự như việc viết predict)
        for i in range(0, detections.shape[2]):
            confidence = detections[0, 0, i, 2]
            class_id = int(detections[0, 0, i, 1])
            # Check for person or cat
            if confidence > 0.6 and class_id == 8:
                box = detections[0, 0, i, 3:7] * np.array([width, height, width, height]) # từ kích thước mô hình sang kích thước ảnh gốc 
                (x1, y1, x2, y2) = box.astype("int")
                # mô hình này sử dụng ảnh xám do kích thước mô hình đầu vào là 224x224x3
                try:
                    catImage = image[y1:y2, x1:x2, 0:3]
                    catImage = cv2.resize(catImage, (224, 224)) # resize ảnh xám do kích thước mô hình đầu vào là 224x224x3
                    catImage = np.expand_dims(catImage, axis=0)
                    catImage = catImage/255.0
                    # Dự đoán mèo 
                    cat_type = self.cat_model.predict(catImage)[0]
                    cat_type = self.cat_list[np.argmax(cat_type)]
                    yield ((x1, y1, x2, y2), confidence, cat_type)
                except: # lỗi xảy ra khi resize.cpp:4152: error: (-215:Assertion failed) !ssize.empty() in function 'cv::resize'
                    None 
    def DrawRect_Text(self, selfInfos, image, rectColor=(125, 240, 84), textColor=(157, 212, 208)):
        for selfInfo in selfInfos: 
            (x1, y1, x2, y2), confidence, cat_type = selfInfo;
            text = "{:.2f}%".format(confidence * 100) + "; Loại mèo: " + cat_type
            y = y1 - 10 if y1 - 10 > 10 else y1 + 10
            cv2.rectangle(image, (x1, y1), (x2, y2), rectColor, 2)
            # chỗ này ghi cái confidence (giống cái accuracy) vào text trên ảnh
            cv2.putText(image, text + " " +str(confidence), (x1, y), cv2.LINE_AA, 0.45, textColor, 2)

            # Lấy thời gian hiện tại
            now = datetime.datetime.now()

            # Định dạng chuỗi thời gian
            time_str = now.strftime("%Y%m%d_%H%M%S")

            # Tạo đường dẫn tệp đầy đủ
            file_path = "images/" + time_str + "_" + cat_type + ".jpg"

            # Lưu hình ảnh
            cv2.imwrite(file_path, image)

            print("Lưu ảnh thành công:", file_path)
        return image
    def Show(self, image):
        cv2.imshow("Output", image)
        cv2.waitKey(0)
    def Run(self, image):
        self.DrawRect_Text(self.AcrFaceDct_Size_Cat(image), image)
        return image

# # video stream initialization
# model = FaceDct_EmoModel()
# vs = cv2.VideoCapture(0) 
# while True: # cái này chơi camera thôi 
#     ret, frame = vs.read()
#     frame = model.Run(frame)
#     cv2.imshow("Press ESC to exit", frame) # show thằng frame này ra 
#     if cv2.waitKey(1) == 27: # 27 trong ASCii # nhấn vào ESC là cúc 
#         break
# # stop capturing
# cv2.destroyAllWindows()
# vs.stop()




model = FaceDct_CatModel() # true là mô hình tự tạo bằng "Train7Emo2.ipynb", False là lấy trên mạng 
cap = cv2.VideoCapture("Vety funny cats.mp4")
if not cap.isOpened(): # Kiểm tra video có mở thành công hay không
    print("Lỗi: Không thể mở file video.")
    exit()
fps = cap.get(cv2.CAP_PROP_FPS) # Lấy fps mặc định của video
frame_delay = 1 / fps * 1000 # Tính thời gian chờ giữa các frame # mili giây
running = True
while running:
    ret, frame = cap.read()
    frame = model.Run(frame)
    if not ret: # nếu hết video 
        break
    # tạo view mới bằng 1/4 kích thước (S) của mặc định
    width, height = frame.shape[1], frame.shape[0]
    new_width = width // 2
    new_height = height // 2

    cv2.namedWindow("Frame", cv2.WINDOW_NORMAL) 
    cv2.resizeWindow("Frame", new_width, new_height) # Thay đổi kích thước cửa sổ
    cv2.imshow("Frame", frame)
    cv2.waitKey(int(frame_delay)) # chờ cho bằng fps
    if cv2.waitKey(1) == 27: # 27 trong ASCii # nhấn vào ESC là cúc 
        running = False
cap.release()
cv2.destroyAllWindows()
