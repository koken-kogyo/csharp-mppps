CREATE OR REPLACE VIEW V_BOM_LEAF_CHILD AS

    /*****************************************************************************************************/
    /*  ID      : V_BOM_LEAF_CHILD                                                                      **/
    /*  Coment  : 親品番から子部品を抽出（Oracle編)                                                     **/
    /*  使用方法: SELECT * FROM V_BOM_LEAF_CHILD WHERE OYAHMCD = 'RA058-62122' ORDER BY KOHMCD          **/
    /*  その他  : 子部品から親部品を抽出する場合は V_BOM_LEAF_PARENT を使用                             **/
    /*  作成者  : コーケン工業 渡辺 2026.05.01                                                          **/
    /*****************************************************************************************************/

WITH STARTER AS (
    SELECT DISTINCT OYAHMCD
    FROM M0520
),
VREC (ROOT_OYA, OYAHMCD, KOHMCD) AS (
    -- 初期行：親品番の一覧
    SELECT
        S.OYAHMCD AS ROOT_OYA,
        S.OYAHMCD,
        M52.KOHMCD
    FROM
        STARTER S
        LEFT JOIN M0520 M52 ON M52.OYAHMCD = S.OYAHMCD

    UNION ALL

    -- 再帰で子品番を辿る
    SELECT
        VREC.ROOT_OYA,
        M52.OYAHMCD,
        M52.KOHMCD
    FROM
        VREC
        INNER JOIN M0520 M52 ON M52.OYAHMCD = VREC.KOHMCD
        INNER JOIN M0500 M50 ON M50.HMCD = M52.KOHMCD
    WHERE
        M50.KZAIKBN != '4'   -- 鋼材区分：母材自身を除外
)
SELECT DISTINCT
    ROOT_OYA as OYAHMCD,
    KOHMCD
FROM
    VREC
WHERE
    KOHMCD IS NOT NULL

