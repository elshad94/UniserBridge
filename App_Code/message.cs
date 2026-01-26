using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for message
/// </summary>
public class message
{
    public static string[]
          msg_selected = new string[] { "Seçilməyib", "Not selected", "не выбрано", "Seçilməyib" },
  msg_pagesize = new string[] { "Sətir sayı", "Page Size", "Размер страницы", "Sayfa boyutu" },
  msg_login = new string[] { "Yanlış İstifadəçi adı və ya parol. Girişinizi kontrol edin.", "Invalid Username or password. Please check your entries.", "Неправильное имя пользователя или пароль. Пожалуйста, проверьте ваши записи.", "Geçersiz kullanıcı adı veya şifre. Girişlerinizi kontrol edin." },
  msg_modules_title = new string[] { "Modullar", "Modules", "Модули", "Modullar" },
  msg_program_logout = new string[] { "Sistemdən Çıxış", "Log Out", "Выйти", "Çıkış" },
   msg_pageopr_new = new string[] { "Əlavə et", "Create", "Создать", "Yeni" },
      msg_pageopr_edit = new string[] { "Dəyişdir", "Edit", "Редактировать", "Değiştir" },
    msg_pageopr_view = new string[] { "Göstər", "View", "Просмотр", "Görüntüle" },
   msg_delete = new string[] { "Siz məlumatın silinməsini təsdiqləyirsinizmi?", "Are you sure you want to delete the selected row?", "Вы уверены, что хотите удалить выбранную строку?", "Siz malumatın silinmesini tasdik ediyormusunuz?" },
  msg_grid_select = new string[] { "Siyahıdan seçim edin.", "Select from list please.", "Выберите из списка, пожалуйста", "Listeden seçim edin." },

    msg_save = new string[] { "Siz məlumatın yadda saxlanılmasını təsdiqləyirsinizmi?", "Are you sure you want to save the changes?", "Вы уверены, что вы хотите сохранить изменения?", "Siz malumatın kayd edilmesini tasdik ediyormusunuz?" },
    msg_input_name = new string[] { "Ad sahəsini daxil edin.", "Input the name field please.", "Пожалуйста, введите название", "Ad sahasını dahil edin." },
    msg_input_docno = new string[] { "Sənəd No sahəsini daxil edin.", "Input the document field please.", "Пожалуйста, введите название", "Dokuman No sahasını dahil edin." },
    msg_input_entrypoint = new string[] { "Giriş nöqtəsi sahəsini daxil edin.", "Input the entry point field please.", "Пожалуйста, введите точка входа", "Giriş noktası sahasını dahil edin." },
    msg_input_exitpoint = new string[] { "Çıxış nöqtəsi sahəsini daxil edin.", "Input the exit point field please.", "Пожалуйста, введите точка выхода", "Çıkış noktası sahasını dahil edin." },
    msg_input_items = new string[] { "Məhsul sahəsini daxil edin.", "Input the items field please.", "Пожалуйста, введите материал", "Malzeme sahasını dahil edin." },
    msg_input_amount = new string[] { "Əmsal sahəsini daxil edin.", "Input the coeficient field please.", "Пожалуйста, введите коэффициент", "Oran sahasını dahil edin." },
    msg_grid_yukalan = new string[] { "Yük göndərən və ya Yük alan qeyd edilməyib.", "No consignor or consignee specified.", "Пожалуйста, введите отправителя и получателя", "No consignor or consignee specified." },
          msg_grid_status = new string[] { "Bu mərhələdə sifarişə müdaxilə edə bilməzsiniz.", "You cannot interfere with the order at this stage.", "На этом этапе вы не можете вмешиваться в заказ." },
    msg_continue = new string[] { "Əməliyyatı davam etdirmək istəyirsinizmi?", "Do you want to continue the operation?", "Хотите продолжить операцию?", "Alanlar eksik" },
    msg_assign = new string[] { "Bu statusda yönləndirmək olmaz", "You cannot redirect in this status", "Вы не можете перенаправить на этот статус", "Bu durumda yönlendiremezsiniz" },
    msg_send = new string[] { "Bu statusda göndərmək olmaz", "You cannot send in this status", "Вы не можете отправить в этом статусе", "Bu durumda gönderemezsiniz" },
    msg_change_error = new string[] { "Bu statusda dəyişdirmək olmaz", "You cannot change in this status", "Вы не можете изменить этот статус", "Bu durumda değiştiremezsiniz" },
    msg_print_error = new string[] { "Bu statusda print etmək olmaz.", "You cannot print in this status.", "Вы не можете печатать в этом статусе.", "Bu durumda print edemezsiniz." },
    msg_save_error = new string[] { "Boş sahələri doldurun.", "Fill in the blanks.", "Заполнить бланки.", "Boş alanları doldurun." };




private string message_text;
        public string Get_Message_text
        {
            get
            {
                return message_text;
            }
            set
            {
                message_text = value;
            }
        }
   
    public void Set_message(int message_code) {

        switch (message_code)
        { 
            case 1:
                //login erroru
                message_text = "Invalid Username or password. Please check your entries.";
                break;

            case 2:
                //bosluqlari dolmayanda cixan mesaj
                message_text = "Please fill in required field";
                break;


            case 3:
                //gridde setr secilmedikde
                message_text = "Please selected row";
                break;

            case 4:
                //ugursuz emeliyyat
                message_text = "Operating unsuccessful";
                break;

            case 5:
                //ugursuz emeliyyat
                message_text = "Please, select stations";
                break;

            case 6:
                //fayl yoxdur
                message_text = "File not found";
                break;




            default:

                message_text= "";
                break;
        
        }
       
    
    
    
    
    
    
    }

}