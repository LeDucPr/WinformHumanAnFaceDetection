import numpy as np
import cv2
from keras.models import load_model
import tensorflow as tf

# emotion_model = load_model('emotion_classification_7classes.hdf5') # emotion 
# input_shape = emotion_model.layers[0].input_shape
# print(input_shape)

# emotion_model = load_model("my_model_2.h5") # emotion 
# input_shape = emotion_model.layers[0].input_shape
# print(input_shape)

class FaceDct_EmoModel:
    def __init__(self):
        self.isModelOfMine = False 
        self.emotion_model = load_model("my_model_2.h5") # emotion of myself 
        self.emotion_model = load_model('emotion_classification_7classes.hdf5') # emotion 
        self.net = cv2.dnn.readNetFromCaffe("weights-prototxt.txt", "res_ssd_300Dim.caffeModel") # SSD + ResNet caffe model 300x300 
        self.emotion_list = ['Angry', 'Disgust', 'Fear', 'Happy', 'Sad', 'Surprise', 'Neutral'] # Danh sách các cảm xúc
        physical_devices = tf.config.experimental.list_physical_devices("GPU");
        if (len(physical_devices) > 0): 
            print (physical_devices) 
    def AcrFaceDct_Size_Emo(self, image):
        gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY) # chuyển sang ảnh đen trắng 
        (height, width) = image.shape[:2]
        blob = cv2.dnn.blobFromImage(cv2.resize(image, (300, 300)), 1.0, (300, 300), (104.0, 177.0, 123.0)) # chuyển sang với trọng số mô hình 
        # Các chỉ số trong detections: 
		# [1]: image_id
		# [0]: class_id
		# [2]: confidence
		# [3]: x_min
		# [4]: y_min
		# [5]: x_max
		# [6]: y_max 
        self.net.setInput(blob) # cho thằng này vào dnn 
        detections = self.net.forward() # xác định đầu ra (cái này tương tự như việc viết predict)
        for i in range(0, detections.shape[2]):
            confidence = detections[0, 0, i, 2]
            if confidence > 0.6: # cái này giống nhưu việc xác định chỉ số accuracy 
                box = detections[0, 0, i, 3:7] * np.array([width, height, width, height]) # từ kích thước mô hình sang kích thước ảnh gốc 
                (x1, y1, x2, y2) = box.astype("int")
                # mô hình này sử dụng ảnh xám do kích thước mô hình đầu vào là 48x48x1
                try:
                    faceImage = gray[y1:y2, x1:x2]
                    faceImage = cv2.resize(faceImage, (48, 48))
                    if (self.isModelOfMine): # Chuyển đổi từ 1 lớp màu sang 3 lớp màu (nếu sử dung mô hình tự tạo )
                        faceImage = cv2.cvtColor(faceImage, cv2.COLOR_GRAY2BGR) 
                    faceImage = np.expand_dims(faceImage, axis=0)
                    faceImage = faceImage/255.0
                    # Dự đoán cảm xúc
                    emotion = self.emotion_model.predict(faceImage)[0]
                    emotion = self.emotion_list[np.argmax(emotion)]
                    yield ((x1, y1, x2, y2), confidence, emotion)
                except: # lỗi xảy ra khi resize.cpp:4152: error: (-215:Assertion failed) !ssize.empty() in function 'cv::resize'
                    None 
    def DrawRect_Text(self, selfInfos, image, rectColor=(125, 240, 84), textColor=(157, 212, 208)):
        for selfInfo in selfInfos: 
            (x1, y1, x2, y2), confidence, emotion = selfInfo;
            # text = "{:.2f}%".format(confidence * 100) + " ( " + str(y2-y1) + ", " + str(x2-x1) + " )"
            text = "{:.2f}%".format(confidence * 100) + "; Emotion: " + emotion
            y = y1 - 10 if y1 - 10 > 10 else y1 + 10
            cv2.rectangle(image, (x1, y1), (x2, y2), rectColor, 2)
            # chỗ này ghi cái confidence (giống cái accuracy) vào text trên ảnh
            cv2.putText(image, text, (x1, y), cv2.LINE_AA, 0.45, textColor, 2)
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

