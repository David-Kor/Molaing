
/*  모든 열거형(enum)을 모아놓은 클래스
 *  사용법 : " EnumInterface.열거형이름.상수이름 "
 *  using SAMPLE = EnumInterface.열거형이름 <- 코드 맨 윗부분에 이런 식으로 추가하면
 *                                                               " SAMPLE.상수이름 " 이런 식으로 줄여서 사용가능
 */

public class EnumInterface
{
    /* DIRECTION_TO_FLOAT */
    /* 모든 캐릭터 애니메이터에서 각각 바라보는 방향의 속성 값을 통일하기 위해 사용 */
    public enum DIRECTION_TO_FLOAT { DOWN, UP, LEFT, RIGHT = 2 }   //좌우 방향은 y축 대칭으로 뒤집기 때문에 같은 값을 갖게 함.


    /* DIRECTION_TO_INT */
    /* 애니메이션 이외에서 사용하는 일반적인 방향 */
    public enum DIRECTTION_TO_INT { DOWN, UP, LEFT, RIGHT }


    /* TYPE_OF_SKILL */
     /* 스킬의 종류를 구분하기 위해 사용 (공격 , 지원 , 채집 순)*/
    public enum TYPE_OF_SKILL { ATTACK, SUPPORT, GATHERING }
}
