using CommonLib;

namespace Frm010_Login
{
    public interface Interface
    {
        /*
        /// <summary>
        /// 作成中表示/非表示設定
        /// </summary>
        /// <param name="visible">表示</param>
        /// <param name="bgColor">背景色</param>
        void SetStateOfLabel5(bool visible, Color bgColor);

        /// <summary>
        /// 進捗状況表示/非表示設定
        /// </summary>
        /// <param name="visible">表示</param>
        void SetVisibleOfSst(bool visible);
        */
        /// <summary>
        /// 進捗状況設定スレッド
        /// </summary>
        /// <param name="progress">進捗率</param>
        void SetStateOfProgressThread(object cmn);
        /*
        /// <summary>
        /// カレンダーの選択値取得
        /// </summary>
        /// <returns>書式化した日付</returns>
        string GetValueOfDateTimePicker1();

        /// <summary>
        /// カレンダーの選択値取得
        /// </summary>
        /// <returns>書式化した日付</returns>
        string GetValueOfDateTimePicker2();

        /// <summary>
        /// カレンダー表示/非表示設定
        /// </summary>
        /// <param name="visible">表示</param>
        void SetVisibleOfDateTimePicker2(bool visible);

        /// <summary>
        /// ウィンドウを閉じる
        /// </summary>
        void CloseWindow();
        */

        void GetCommonClass(ref Common cmnIntfc);

    }
}
