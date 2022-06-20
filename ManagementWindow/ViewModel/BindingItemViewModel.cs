using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows;
using ManagementWindow.BaseClass;
using ManagementWindow.SQL;
using ManagementWindow.View;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Net;
using BaseClass.Tool;

namespace ManagementWindow.ViewModel
{
    public class BindingItemViewModel : ObservableObject
    {
        public AppData AppData { get; set; } = AppData.Instance;
        public string ItemNo { get; set; }
        private DateTime? starttime;
        public DateTime? Starttime { get => starttime; set => SetProperty(ref starttime, value); }
        private DateTime? endtime;
        public DateTime? Endtime { get => endtime; set => SetProperty(ref endtime, value); }
        /// <summary>
        /// 选择时间
        /// </summary>
        /// <returns></returns>
        public List<Staff> SelectedDateChanged()
        {
            string sta = "";
            string end = "";
            if (Starttime != null)
            {
                sta = ((DateTime)Starttime).ToString("yyyy/MM/dd");
            }
            if (Endtime != null)
            {
                end = ((DateTime)Endtime).ToString("yyyy/MM/dd");
            }
            return SqlAssociated.CmdAllPersonndelGetUI(Starttime.ToString() ?? "", Endtime.ToString() ?? "") ;
        }
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="staff">选中的行</param>
        /// <param name="ItemNo">项目号</param>
        public void DoubleClickBinding(Staff staff, string ItemNo)
        {
            ItemStaff itemStaff = new ItemStaff
            {
                ItemNo = ItemNo,
                ID = staff.ID,
                Starttime = ((DateTime)(Starttime??DateTime.Now)).ToString("yyyy/MM/dd"),
                EndTime = ((DateTime)(Endtime ?? DateTime.Now)).ToString("yyyy/MM/dd"),
            };
            if (MessageBox.Show(string.Format("请确认绑定时间为{0}--{1}", itemStaff.Starttime, itemStaff.EndTime),
                "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int i = SqlAssociated.AddItemStaffFromBindingItemWindow(itemStaff);
                if (i != 0)
                {
                    AppData.MainWindow.container.Content = new ItemManagementWindow();

                    CNetLog.Instance.WriteLog("BindingItem ：" + JsonHelper<ItemStaff>.GetJsonStr(itemStaff));

                   if ( MessageBox.Show(string.Format("绑定成功，是否自动发送邮件通知{0}",staff.Name),"提示",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        string emailText = string.Format("此为系统邮件请勿回复————————————————————" + Environment.NewLine +
                            "{0}您好：" + Environment.NewLine +
                            "    {1}安排您于{2}——{3}去{4}实施工作，请尽快协调时间交接手头工作！" + Environment.NewLine +
                            "如有疑问请联系您的主管确认。感谢您的理解。", staff.Name,
                            AppData.CurrentUser.username,
                            itemStaff.Starttime,
                            itemStaff.EndTime);
                        bool bo= SendMailByPlainFormat(staff.Email, "", "", emailText);
                        if (bo)
                        {
                            MessageBox.Show("发送成功");
                            CNetLog.Instance.WriteLog("Send Email Success");
                        }
                        else
                        {
                            MessageBox.Show("发送失败，请联系管理人员确认！");
                        }

                        
                    }
                }
            }
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="toAddr">收件人人地址</param>
        /// <param name="Cc">抄送地址</param>
        /// <param name="content">邮件内容</param>
        /// <param name="attach">附件内容</param>
        public bool SendMailByPlainFormat(string toAddr, string Cc, string attach,string content)
        {
            string sendEmail = "a1410648838@163.com";//发件人
            string sendpwd = "UQJBYFDXWYMSJIRB";//密码

            MailMessage mailobj = new MailMessage();
            mailobj.From = new MailAddress(sendEmail);
            mailobj.To.Add(toAddr);
            if (Cc != "")
                mailobj.CC.Add(Cc);  //抄送
            mailobj.Subject = "工作安排"; //主题
            mailobj.Body = content; //内容
            mailobj.IsBodyHtml = true; //内容是否可以为html形式
            mailobj.BodyEncoding = Encoding.Default;

            //附件
            if (attach != "")
            {
                char[] delim = new char[] { ';' };
                foreach (string substr in attach.Split(delim))
                {
                    Attachment MyAttach = new Attachment(substr);
                    //MailAttachment MyAttach = new MailAttachment(substr);
                    mailobj.Attachments.Add(MyAttach);
                }
            }

            SmtpClient smtpclient = new SmtpClient("smtp.163.com");
            // smtpclient.Credentials = CredentialCache.DefaultNetworkCredentials;
            smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //如果服务器支持安全连接，则将安全连接设为true
            smtpclient.EnableSsl = true;
            try
            {
                //是否使用默认凭据，若为false，则使用自定义的证书，就是下面的networkCredential实例对象
                smtpclient.UseDefaultCredentials = true;

                //指定邮箱账号和密码,需要注意的是，这个密码是你在QQ邮箱设置里开启服务的时候给你的那个授权码
                NetworkCredential networkCredential = new NetworkCredential(sendEmail, sendpwd);
                smtpclient.Credentials = networkCredential;

                //发送邮件
                smtpclient.Send(mailobj);

            }
            catch (System.Net.Mail.SmtpException ex)
            {
                CNetLog.Instance.WriteLog("Send Email Fail Because ：" + ex.Message);
                return false;
            }
            return true;
        }
    }
}
