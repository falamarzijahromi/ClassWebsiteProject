using Common.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteHttp.Validators
{
    public class UserNationalCodeValidator : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            IdentityResult result;

            if (check_code(user.NationalCode))
            {
                result = IdentityResult.Success;
            }
            else
            {
                var error = new IdentityError() { Description = "National Code is not valid." };

                result = IdentityResult.Failed(error);
            }

            return Task.FromResult(result);
        }

        bool check_code(string nationalCode)
        {
            bool result;

            try
            {

                //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
                var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
                if (allDigitEqual.Contains(nationalCode)) return false;


                //عملیات شرح داده شده در بالا
                var chArray = nationalCode.ToCharArray();
                var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
                var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
                var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
                var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
                var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
                var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
                var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
                var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
                var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
                var a = Convert.ToInt32(chArray[9].ToString());

                var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
                var c = b % 11;

                return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
            }
            catch { result = false; }

            return result;
        }
    }
}
