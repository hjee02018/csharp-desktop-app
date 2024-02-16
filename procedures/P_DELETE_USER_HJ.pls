CREATE OR REPLACE PROCEDURE HMX_KCTC.P_DELETE_USER_HJ (
    PV_JSON_I        IN  VARCHAR2,
    PV_JSON_O        OUT VARCHAR2,
    PV_RETCOD_O      OUT INTEGER,
    PV_RETMSG_O      OUT VARCHAR2)
/*==============================================================================================================================================
PROCEDURE NAME   : P_DELETE_USER_HJ
CREATE DATE      : 2024/02/15
ALTER DATE       : 
WRITER           : YHJ
MODIFIER         : 
INPUT PARAMETER  : @PV_JSON_I
OUTPUT PARAMETER : @PV_JSON_O
                   @PV_RETCOD_O - 프로시져 결과 코드
                   @PV_ERRMSG_O - 에러 메세지
==============================================================================================================================================*/
IS
    lv_user_id     T_USER.USER_ID%TYPE;
    lv_id_t_user   T_USER.ID_T_USER%TYPE;

    lv_count       INTEGER;

    USER_ERROR     EXCEPTION;
BEGIN
    SELECT JSON_VALUE (PV_JSON_I, '$.USER_ID') INTO lv_user_id FROM DUAL;

    -- 사용자중복 검사
    SELECT COUNT (*)
      INTO lv_count
      FROM HMX_KCTC.T_USER
     WHERE USER_ID = lv_user_id;

    -- 동일ID가 없으면 USER_ERROR를 발생시킨다.
    IF lv_count = 0
    THEN
        RAISE USER_ERROR;
    ELSE
        SELECT ID_T_USER
          INTO lv_id_t_user
          FROM HMX_KCTC.T_USER
         WHERE USER_ID = lv_user_id;

        UPDATE HMX_KCTC.T_USER
		  SET USE_YN = '2'
		 WHERE ID_T_USER = lv_id_t_user;
    END IF;

    PV_RETCOD_O := 0;
    PV_RETMSG_O := '사용자 정보 삭제 완료';
    PV_JSON_O := F_JSON_RESULT (PV_RETCOD_O, PV_RETMSG_O);

    COMMIT;
EXCEPTION
    WHEN USER_ERROR
    THEN
        PV_RETCOD_O := 2;
        PV_RETMSG_O := ' 사용자를 찾을 수 없습니다.';

        PV_JSON_O := F_JSON_RESULT (PV_RETCOD_O, PV_RETMSG_O);

        ROLLBACK;
    WHEN OTHERS
    THEN
        PV_RETCOD_O := SQLCODE;

        PV_RETMSG_O :=
            SF_MESSAGE_HEADER ($$PLSQL_UNIT, $$PLSQL_LINE) || SQLERRM;
        PV_JSON_O := F_JSON_RESULT (PV_RETCOD_O, PV_RETMSG_O);
        ROLLBACK;
END;