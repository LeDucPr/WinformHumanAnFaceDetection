import numpy as np
import cv2
import tensorflow as tf

class FaceDct_EmoModel:
    def __init__(self):
        # self.net = cv2.dnn.readNetFromCaffe("weights-prototxt.txt", "res_ssd_300Dim.caffeModel") # SSD + ResNet caffe model 300x300 
        self.net = cv2.dnn.readNetFromCaffe("deploy.prototxt", "mobilenet_iter_73000.caffemodel") # SSD + ResNet caffe model 300x300 
        physical_devices = tf.config.experimental.list_physical_devices("GPU");
        if (len(physical_devices) > 0): 
            print (physical_devices) 
    def AcrFaceDct_Size_Emo(self, image):
        gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY) # chuyển sang ảnh đen trắng 
        (height, width) = image.shape[:2]
        # blob = cv2.dnn.blobFromImage(cv2.resize(image, (300, 300)), 1.0, (300, 300), (104.0, 177.0, 123.0)) # chuyển sang với trọng số mô hình 
        blob = cv2.dnn.blobFromImage(cv2.resize(image, (300, 300)), 0.007843, (300, 300), (127.5, 127.5, 127.5))
        self.net.setInput(blob) # cho thằng này vào dnn 
        detections = self.net.forward() # xác định đầu ra (cái này tương tự như việc viết predict)
        for i in range(0, detections.shape[2]):
            confidence = detections[0, 0, i, 2]
            class_id = int(detections[0, 0, i, 1])
            # Check for person or cat
            if confidence > 0.6 and class_id == 15:
                box = detections[0, 0, i, 3:7] * np.array([width, height, width, height]) # từ kích thước mô hình sang kích thước ảnh gốc 
                (x1, y1, x2, y2) = box.astype("int")
                # mô hình này sử dụng ảnh xám do kích thước mô hình đầu vào là 48x48x1
                try:
                    yield ((x1, y1, x2, y2), confidence)
                except: # lỗi xảy ra khi resize.cpp:4152: error: (-215:Assertion failed) !ssize.empty() in function 'cv::resize'
                    None 
    def DrawRect_Text(self, selfInfos, image, rectColor=(125, 240, 84), textColor=(157, 212, 208)):
        for selfInfo in selfInfos: 
            (x1, y1, x2, y2), confidence = selfInfo;
            y = y1 - 10 if y1 - 10 > 10 else y1 + 10
            cv2.rectangle(image, (x1, y1), (x2, y2), rectColor, 2)
            # chỗ này ghi cái confidence (giống cái accuracy) vào text trên ảnh
            cv2.putText(image, "cat "+str(confidence), (x1, y), cv2.LINE_AA, 0.45, textColor, 2)
        return image
    def Show(self, image):
        cv2.imshow("Output", image)
        cv2.waitKey(0)
    def Run(self, image):
        self.DrawRect_Text(self.AcrFaceDct_Size_Emo(image), image)
        return image

# video stream initialization
model = FaceDct_EmoModel()
vs = cv2.VideoCapture(0) 
while True: # cái này chơi camera thôi 
    ret, frame = vs.read()
    frame = model.Run(frame)
    cv2.imshow("Press ESC to exit", frame) # show thằng frame này ra 
    if cv2.waitKey(1) == 27: # 27 trong ASCii # nhấn vào ESC là cúc 
        break
# stop capturing
cv2.destroyAllWindows()
vs.stop()

