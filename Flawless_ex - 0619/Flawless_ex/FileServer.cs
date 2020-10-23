using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flawless_ex
{   
    class FileServer
    {
        public enum Filetype :int{ 
            ID = 1,
            Antiquelicense = 2,
            TaxCertification = 3,
            ResidenceCard = 4,
            SealCertification = 5,
            AolFinancialShareholder = 6,
            RegisterCopy = 7
        };

        public string ShowImage(Filetype type)
        {
            string path;
            path = "";
            return path;
        }

        public string UploadImage(string path, Filetype type)
        {
            string path1 = path;
            string path2 = "";
            
            string dir = "";
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            switch ((int)type)
            {
                case 1:
                    //身分証明書または顔つき身分証
                    dir = "id";
                    break;
                case 2:
                    //古物商許可証
                    dir = "antique_license";
                    break;
                case 3:
                    //納税証明書
                    dir = "tax_certificate";
                    break;
                case 4:
                    //在留カード
                    dir = "residence_card";
                    break;
                case 5:
                    //印鑑証明
                    dir = "seal_certification";
                    break;
                case 6:
                    //定款、決算書、株主構成のいずれか
                    dir = "aol_financial_shareholder";
                    break;
                case 7:
                    //登記簿謄本
                    dir = "register_copy";
                    break;
                default:
                    break;
            }

            if (dir != "")
            {
                path2 = @"\\192.168.152.164\Flawless_test\" + dir + @"\" +dir + timestamp + ".png";
            }
            System.IO.File.Copy(path1, path2, true);
            return path2;
        }
    }
}
