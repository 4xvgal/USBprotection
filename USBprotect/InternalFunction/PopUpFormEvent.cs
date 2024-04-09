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
        public abstract void PopUpForm();
      
    }

    public class UnauthorizedUsbFormEvent : FormEventBase // 추상 클래스를 상속받은 클래스
    {
        public override void PopUpForm()
        {
            Form2 form2 = new Form2("비인가 USB 접근이 감지되었습니다.");
            form2.Show(); // 폼을 표시합니다.
        }
    }

    public class AuthorizedUsbFormEvent: FormEventBase // 추상 클래스를 상속받은 클래스
    {
        public override void PopUpForm()
        {
            Form2 form2 = new Form2("허가된 USB 입니다.");
            form2.Show(); // 폼을 표시합니다.
        }
    }

    public class RemoveUsbFormEvent : FormEventBase // 추상 클래스를 상속받은 클래스
    {
        public override void PopUpForm()
        {
            Form2 form2 = new Form2("USB가 제거되었습니다.");
            form2.Show(); // 폼을 표시합니다.
        }
    }

    // 이 밑으로 필요하신 이벤트 쭉죾  
}
