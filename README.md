# OXCFontConverter
將輸入的文字依照指定的字型轉換成 OpenXCom可使用的字型圖集，包含
* `FontBig.png`: 每字 16x16，使用 12pixel 字型轉換
* `FontSmall.png` : 每字 8x9，使用 8pixel 字型轉換
* `FontGeoBig.png` : 每字 8x9，使用 8pixel 字型轉換
* `FontGeoSmall.png` : 每字 8x7，使用 7pixel 字型轉換

## FontConfig.json 欄位說明
* `Common4808HanT`: 已預先提供教育部4808個常用正體字。程式啟動時會自動帶入
* `Region`: 地區名稱。轉出 PNG 時，會自動加到檔名去。ex. FontBig_zh-TW.png
* `GlyphTiles[]`: 分別為 FontBig, FontSmall, FontGeoBig, FontGeoSmall 的設定。包括預設字型名稱、字型高度、寬度與大小以及渲染後字體的相對偏移位置
* `AllowedFontFamily[]`: 可選的字型。程式自動抓取Windows安裝過的所有字型，可透過這個陣列過濾出想要的字型
