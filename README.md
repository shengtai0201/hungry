# 我想要最佳的人工智慧解決方案(I want the best artificial intelligence solution.)
----------
TODO

### 系統整合
- TensorFlow Serving，負責模型的持續訓練與推論。
- 伺服器端(server)，整體架構的資訊轉運站。
  關於針對 TensorFlow Serving 實作gRPC client 參考了 Google 的 [Client API(REST)](https://www.tensorflow.org/tfx/serving/api_rest)
- 網頁前端(client)，後臺管理系統。
- 手機前端(mobile)，服務應用的呈現。
- 機器學習，以 AutoML 的方式選定模型。

### 開發環境
- Visual Studio Community 2022 Preview
- Visual Studio Code
- WSL: Ubuntu 22.04.3 LTS
- Anaconda3 2003.09-0
- Python 3.11.7
- TensorFlow 2.15.0
- AutoKeras 1.1.0
- Android Studio Jellyfish
- BlueStacks (Android 11 64-bit)

### 系統營運
- 系統安裝
- 伺服器管理
- 備份還原機制