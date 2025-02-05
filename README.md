# 切削生産計画システム  
- [KMD001SF] csharp-mppps  

## 概要  
- 切削生産計画システム  

## 開発環境  
- C# .NET Framework v4.6.1 Windowsアプリケーション  

## 参照設定  
- DecryptPassword.dll  
- Oracle.ManagedDataAccess.dll  
- LumenWorks.Framework.IO
- MySql.Data.dll  
  (\packages\MySql.Data.8.0.32.1\lib\net452)  

## メンバー  
- y.futohashi  
- y.watanabe  

## プロジェクト構成  
~~~  
./  
│  .gitignore                                  # ソース管理除外対象  
│  MPPPS.sln                                   # Visual Studio Solution ファイル  
│  README.md                                   # このファイル  
│  
├─ CommonLib  
│  │  App.config                              # アプリケーション設定ファイル  
│  │  Common.cs                               # 共通設定ファイル  
│  │  FileAccess.cs                           # ファイルアクセス  
│  └  Program.cs                              # メイン関数  
│      
├─ Frm010_Login                               # ログイン  
│  
├─ Frm020_MainMenu                            # メイン メニュー  
│  
├─ Frm030_MasterMaint                         # マスタ メンテナンスメニュー  
│  
├─ Frm031_CutProcUserMstMaint                 # 利用者マスタ メンテ  
│  
├─ Frm032_ChipMstMaint                        # 刃具マスタ メンテ  
│  
├─ Frm033_EqMstMaint                          # 設備マスタ メンテ  
│  
├─ Frm034_CodeSlipMstMaint                    # コード票マスタ メンテ  
│  
├─ Frm040_OrderCtrl                           # オーダー管理メニュー  
│  
├─ Frm041_CreateOrder                         # 切削オーダーの作成  
│  
├─ Frm042_OrderEqualize                       # 切削オーダーの平準化  
│  
├─ Frm043_CreateAddOrder                      # 追加オーダーの作成  
│  
├─ Frm044_OrderInfo                           # 切削オーダー情報  
│  
├─ Frm045_MfgProgress                         # 加工進捗状況表示  
│  
├─ Frm050_MfgCtrl                             # 製造管理メニュー  
│  
├─ Frm051_OrderDirection                      # 切削オーダー指示書  
│  
├─ Frm052_FormsPrinting                       # 帳票出力  
│  
├─ Frm070_ReceiptCtrl                         # 実績管理メニュー
│  
├─ Frm071_ReceiptProc                         # 切削ストア受入実績処理  
│  
├─ Frm072_ReceiptInfo                         # 切削ストア受入実績情報表示  
│  
├─ Frm073_EntryShipRes                        # EM への実績入力  
│  
├─ Frm080_MatlCtrl                            # 材料管理メニュー  
│  
├─ Frm081_MatlInvList                         # 材料在庫一覧  
│  
├─ Frm082_MatlOrder                           # 材料発注処理  
│  
├─ Frm083_MatlInsp                            # 材料検収  
│  
├─ Frm090_CutStore                            # 切削ストアメニュー  
│  
├─ Frm091_CutStoreDelv                        # 切削ストア出庫  
│  
├─ Frm092_CutStoreInvInfo                     # 切削ストア在庫情報  
│  
├─ Frm100_VerInfo                             # バージョン情報  
│  
├─ packages  
│  │  DecryptPassword.dll                     #   
│  │  Oracle.ManagedDataAccess.dll            #   
│  └  MySql.Data.8.0.32.1                     #   
│      
├─ Resources  
│  ├─ conf                                   # アプリケーション設定ファイル格納フォルダ  
│  ├─ docs  
│  ├─ icons  
│  ├─ pictures  
│  └─ sounds                                 # メイン関数  
│      
└─ specification  
        [KXXxxxXX] xxx 機能仕様書_Ver.1.0.0.0.xlsx  
        
~~~  

## アセンブリ情報  

- 著作権： © 2023 Koken Kogyo Co., Ltd.


