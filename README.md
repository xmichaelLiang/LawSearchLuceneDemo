# LawSearchLuceneDemo
# 🔍 LawSearchJiebaNet8_MVC  
.NET 8 MVC + Lucene.Net 3.0.3 + JiebaNet 中文全文檢索練習

本專案示範如何使用 ASP.NET Core MVC 搭配 Lucene.Net 3.0.3 和 Jieba 中文斷詞實作全文檢索功能。資料來源可從資料庫擴充，索引離線建立、搜尋快速回應。

---

## 🧰 使用技術

- [.NET 8 ASP.NET Core MVC](https://learn.microsoft.com/aspnet/core)
- [Lucene.Net 3.0.3](https://www.nuget.org/packages/Lucene.Net/3.0.3) — C# 全文索引引擎
- [JiebaNet.Segmenter](https://github.com/anderscui/jieba.NET) — 中文斷詞
- [Bootstrap 5 (optional)](https://getbootstrap.com/) — 前端樣式美化

---

## 📂 專案結構

```
LawSearchJiebaNet8_MVC/
├── Controllers/
│   └── SearchController.cs          # 控制搜尋與建立索引
├── Models/
│   └── LawClause.cs                 # 法條模型（Id、Title、Content）
├── Services/
│   ├── LuceneIndexer.cs            # Lucene 建立/查詢邏輯
│   └── JiebaHelper.cs              # 中文斷詞處理
├── Views/
│   └── Search/
│       ├── Index.cshtml            # 搜尋輸入頁面
│       └── Result.cshtml           # 搜尋結果顯示
├── Resources/                      # Jieba 中文模型檔（需手動複製）
├── index/                          # Lucene 索引目錄（自動產生）
└── README.md
```

---

## 📦 安裝套件

請安裝下列 NuGet 套件：

```bash
dotnet add package Lucene.Net --version 3.0.3
dotnet add package JiebaNet.Segmenter
dotnet add package System.Configuration.ConfigurationManager
```

---

## 📁 Jieba 中文模型 Resources 檔案

JiebaNet 在初始化時會載入以下檔案：

```
dict.txt
hmm_model.utf8
idf.txt
prob_trans.json
userdict.txt
```

請從 NuGet 快取複製：

```
%USERPROFILE%\.nuget\packages\jiebanet.segmenter\0.0.8\resources\
```

複製整個 `resources` 資料夾到專案的 `Resources/` 資料夾，並修改程式初始化時指定該路徑：

```csharp
var baseDir = Path.Combine(AppContext.BaseDirectory, "Resources");
segmenter = new JiebaSegmenter(baseDir);
```

建議在 `.csproj` 加上：

```xml
<ItemGroup>
  <None Update="Resources\**\*.*">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

---

## 🧪 功能示範

### 1. 建立索引

瀏覽：

```
/Search/Build
```

會將內建的假資料（或日後從 DB 撈資料）進行 Jieba 斷詞，並建立 Lucene 索引到 `/index` 資料夾。

---

### 2. 搜尋法條

瀏覽：

```
/Search/Index
```

輸入關鍵字（例如：「犯罪」、「法人」、「民事」）→ 按下查詢 → 顯示包含該關鍵詞的法條。

---

## 🧠 實務運作建議

### 建議流程（企業實務）

1. 從 DB 撈出巨量資料 → 排程斷詞 + 建立 Lucene 索引檔（離線進行）
2. 使用者輸入關鍵字 → Jieba 斷詞
3. 查詢 Lucene 索引 → 取得匹配 ID
4. 用 ID 回 DB 撈取詳細資料（或快取）

這樣可大幅減少 DB 壓力、提升搜尋速度。

---

## 🗃 待辦功能（可擴充）

- [ ] 增加分頁顯示結果
- [ ] 排程每日重建索引
- [ ] 索引檔異常自動重建
- [ ] 支援 Elasticsearch 分散式部署

---

## 📜 授權 License

MIT License
