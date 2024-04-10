using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//팝업폼2의 이벤트를 정의한 클래스입니다.
//폼을 띄우는 이벤트를 정의합니다.
// 추상화 기법을 이용해서 유사한 기능을 하는 클래스들을 묶어서 관리합니다.


namespace USBprotect.InternalFunction
{
    public abstract class FormEventBase // 추상 클래스
    {
        protected static Form1 form1instance;

        public void GetForm(Form1 form1){form1instance = form1;}   
        public abstract void PopUpForm();
      
    }

 

    public class UnauthorizedUsbFormEvent : FormEventBase // 추상 클래스를 상속받은 클래스
    {
        public override void PopUpForm()
        {
            form1instance.OpenForm2("비인가 USB 접속.");
        }
    }

    public class AuthorizedUsbFormEvent: FormEventBase // 추상 클래스를 상속받은 클래스
    {
        public override void PopUpForm()
        {
            form1instance.OpenForm2("블랙 리스트 장치 입니다!!");
        }
    }

    public class RemoveUsbFormEvent : FormEventBase // 추상 클래스를 상속받은 클래스
    {
        public override void PopUpForm()
        {
            form1instance.OpenForm2("USB 제거됨");
        }
    }

    // 이 밑으로 필요하신 이벤트 쭉죾  
}
