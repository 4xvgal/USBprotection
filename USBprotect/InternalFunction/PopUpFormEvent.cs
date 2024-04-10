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
        protected static Form1 form1instance; // Form1 인스턴스를 저장할 변수

        public void GetForm(Form1 form1) { form1instance = form1; }  // Form1 으로 부터 객체를 넘겨받아서 form1 에서 컨트롤 할 수 있도록  
        public abstract void PopUpForm(); // 추상 메서드

    }

    public class UnauthorizedUsbFormEvent : FormEventBase // 추상 클래스를 상속받은 클래스 (처음보는 장치일 경우))
    {
        public override void PopUpForm()
        {
            //invoke 란 다른 스레드에서 UI 컨트롤에 접근할 때 사용하는 메서드 
            if (form1instance.InvokeRequired)  // 기존 코드에서 InvokeRequired를 사용하여 Invoke가 필요한지 확인
            {
                form1instance.Invoke(new Action(() => form1instance.OpenForm2("비인가 USB 접속.")));
            }
            else // Invoke가 필요없는 경우
            {
                form1instance.OpenForm2("비인가 USB 접속.");
            }
        }
    }

    public class AuthorizedUsbFormEvent : FormEventBase // 추상 클래스를 상속받은 클래스 ( 블랙리스트 장치일 경우)
    {
        public override void PopUpForm()
        {
            //invoke 란 다른 스레드에서 UI 컨트롤에 접근할 때 사용하는 메서드 
            if (form1instance.InvokeRequired)  // 기존 코드에서 InvokeRequired를 사용하여 Invoke가 필요한지 확인
            {
                form1instance.Invoke(new Action(() => form1instance.OpenForm2("블랙리스트 장치 식별!!")));
            }
            else // Invoke가 필요없는 경우
            {
                form1instance.OpenForm2("블랙리스트 장치 식별!!");
            }
        }
    }

    public class RemoveUsbFormEvent : FormEventBase // 추상 클래스를 상속받은 클래스 (usb 제거 이벤트)
    {
        public override void PopUpForm()
        {
            //invoke 란 다른 스레드에서 UI 컨트롤에 접근할 때 사용하는 메서드 
            if (form1instance.InvokeRequired)  // 기존 코드에서 InvokeRequired를 사용하여 Invoke가 필요한지 확인
            {
                form1instance.Invoke(new Action(() => form1instance.OpenForm2("usb 제거됨")));
            }
            else // Invoke가 필요없는 경우
            {
                form1instance.OpenForm2("usb 제거됨");
            }
        }
    }
}


