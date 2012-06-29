open System
open System.Configuration
open System.IO;
open System.Net
open System.Net.Mail
open ProFSharp.Process

let ExecuteSvnlook path repo rev command =
    let arguments = String.Format("{0} -r {1} \"{2}\"", command, rev, repo)
    String.Format("\r\n\r\nsvnlook {0}\r\n", arguments) 
        + ProcessHelper.ExecuteProcess path arguments

[<EntryPoint>]    
let main args = 
    try
        let repo = args.[0]
        let rev = args.[1]
        
        let pathToSvnLook = ConfigurationManager.AppSettings.["ProFSharp.SvnMail.Snv.PathToSvnLook"]
        let svnlook = ExecuteSvnlook pathToSvnLook repo rev
        let svnlookInfo = svnlook "info" 
        let svnlookChanged = svnlook "changed"
        
        let smtpHost = ConfigurationManager.AppSettings.["ProFSharp.SvnMail.Smtp.Host"]
        let userName = ConfigurationManager.AppSettings.["ProFSharp.SvnMail.Smtp.UserName"]
        let password = ConfigurationManager.AppSettings.["ProFSharp.SvnMail.Smtp.Password"]
        let smtp = new SmtpClient(smtpHost)
        smtp.Credentials <- new NetworkCredential(userName, password)
        
        let mailfrom = ConfigurationManager.AppSettings.["ProFSharp.SvnMail.Message.From"]
        let mailto = ConfigurationManager.AppSettings.["ProFSharp.SvnMail.Message.To"]
        let subject = ConfigurationManager.AppSettings.["ProFSharp.SvnMail.Message.Subject"]
        let bodyFormat = ConfigurationManager.AppSettings.["ProFSharp.SvnMail.Message.Body"]
        let bodyMessage = String.Format(bodyFormat, repo, rev)
        
        let body = bodyMessage + svnlookInfo + svnlookChanged
        
        use msg = new MailMessage(mailfrom, mailto, subject, body)
        smtp.Send(msg);
        0
    with
        _ as ex
            ->  Console.Error.WriteLine("Error sending notification email.")
                Console.Error.WriteLine(ex)
                1

