CREATE OR REPLACE VIEW V_BOM_LEAF_PARENT AS

    /*****************************************************************************************************/
    /*  ID      : V_BOM_LEAF_PARENT                                                                     **/
    /*  Coment  : 子品番から親部品を抽出（Oracle編)                                                     **/
    /*  使用方法: SELECT * FROM V_BOM_LEAF_PARENT WHERE KOHMCD = '129486-59140B'                        **/
    /*  その他  : 親部品から子部品を抽出する場合は V_BOM_LEAF_CHILD を使用                              **/
    /*  作成者  : コーケン工業 渡辺 2026.05.01                                                          **/
    /*****************************************************************************************************/

WITH VREC (OYAHMCD, KOHMCD) AS (
    -- 初期行：全品番を対象
    SELECT 
        OYAHMCD,
        KOHMCD
    FROM
        M0520

    UNION ALL

    -- 再帰で親を辿る
    SELECT
        M52.OYAHMCD,
        VREC.KOHMCD
    FROM
        VREC
        INNER JOIN M0520 M52 ON M52.KOHMCD = VREC.OYAHMCD
)
SELECT DISTINCT
    OYAHMCD,
    KOHMCD
FROM (
    SELECT
        OYAHMCD,
        KOHMCD,
        CASE WHEN EXISTS (
            SELECT 1 FROM VREC LEAF WHERE LEAF.KOHMCD = VREC.OYAHMCD
        )
        THEN 0 ELSE 1 END AS ISLEAF
    FROM
        VREC
)
WHERE ISLEAF = 1;

