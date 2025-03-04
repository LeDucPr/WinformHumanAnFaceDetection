# WinformHumanAnFaceDetection 
 + EmguCV 4.6 (Update .net8)
 + Khử độ trễ load thư viện Emgu
 + Sử dụng Single Shot Detection (SSD)
 + Hiệu suất được sử dụng trên Gpu lớn 
## Thiết kế (Đơn luồng) Sử dụng hiệu quả với Camera
 ComboBox (Cpu/Nvidia_GPU_Name(Nếu có)) ------------------------------------------------------------------------------------------------------->|+-----> Handle --> Lock(Image) + Clone --> Show(Image) --> GC
 |                                                                                                                                              |+--------------------------------------------+|
 -> Mặc định sử dụng Cpu ---------------------> Queue.Dequeue()? ------------------------------------------------------------------------------>|+--> if Select_item --> Lock(Queue) + Wait lastImage handled 
 |                                                ?|                                                                                            |                                              |
 -> Khi có thiết bị có Gpu Nvidia(>Gtx630) --> Queue.Peek()? --> Hanlder(Gpu) --> Garbage Collection() + Enable Convert to Gpu on Combobox --->|+                                        Convert(Cpu/Gpu)
                                                  |                                                                                                                                            |
   (Queue<Image>) ------------------------------>|+|<------------------------------------------------------------------------------------------------------------------------------------------+
     |
   Image
     |
-> Input (Camera)
## Thiết kế (Đa luồng) Sử dụng hiệu quả cho việc sử dụng Mô hình kết hợp Folder(Tree)
ComboBox (Cpu/Nvidia_GPU_Name(Nếu có)) -------------------------------------------->|
 |                                                                                  |                                                           
 -> Mặc định sử dụng Cpu ---------------------------------------------------------->|+--> Threads --> Lock(Queue)+Dequeue --> Handle ----------> Save to Folder(Tree) / Others process
 |---------------> Tăng số lượng luồng (Threads) xử lý trên thiết bị--------------->|+                                          |
 -> Khi có thiết bị có Gpu Nvidia(>Gtx630) --> Enable Convert to Gpu on Combobox -->|+--> if Select_item --> (Wait lastImages handled) --> Convert(Cpu/Gpu)
                                                  |                                                                                              |
   (Queue<Image>) ------------------------------>|+|<--------------------------------------------------------------------------------------------+
     |
   Images
     |
-> Input (Quét tất cả Image File) + Thread Input giới hạn số lượng ảnh trên Queue + sleep()

### Sử dụng 
  + Được thiết kế như một bộ chuyển đổi chung. Nếu bạn muốn sử dụng cho nhiều phần việc khác nhau hoặc thiết kế lại để chia sẻ tài nguyên trên Ram ổn định hơn thì có thể tạo riêng cấu trúc Emgu thành 1 thư viện và tách riêng Usage.
  + Cấu trúc xử lý chống độ trễ cho việc load thư viện EmguCV (>2GB). Các thiết bị mới hơn sử dụng Ram DDR5 có thể không gây ra độ trễ này.
  + Có thể kết hợp Image sau khi xử lý đưa vào một Queue khác kết hợp với các thư viện message broker để trở thành bộ truyền tải RealTime
  + Sử dụng lưu bộ cắt ảnh sử dụng (SSD) + lưu theo cấu trúc tên file và đường dẫn cũ  
     
