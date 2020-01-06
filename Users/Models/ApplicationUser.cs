using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Users.Models {
    /// <summary>
    /// 自定义的User Class继承IdentityUser，在此扩展自定义User属性
    /// </summary>
    public class ApplicationUser : IdentityUser 
    {
        public override string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CurrentNumberPrizes { get; set; }
        public int MaxNumberPrizes { get; set; }
        public Gender Gender { get; set; }
        public string SocialMedia { get; set; }
        public string CompanyName { get; set; }
        public int CompanySize { get; set; } = 0;
        public string JobTitle { get; set; }
        public Country Country { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public Ethnicity Ethnicity { get; set; }
        public string HouseHoldIncome { get; set; }       
    }

    public enum Gender
    {
        None,
        Male,
        Female
    }
    public enum Ethnicity
    {
        Other,
        White,
        Black,
        Asian,
        [Display(Name = "Native American")]
        Native_American,
        Hispano
    }

    public enum Country
    {
        [Display(Name = "United States of America")]
        United_States_of_America,
        Afghanistan,
        Albania,
        Algeria,
        Andorra,
        Angola,
        Argentina,
        Armenia,
        Australia,
        Austria,
        Azerbaijan,
        Bahamas,
        Bahrain,
        Bangladesh,
        Barbados,
        Belarus,
        Belgium,
        Belize,
        Benin,
        Bhutan,
        Bolivia,
        [Display(Name = "Bosnia and Herzegovina")]
        Bosnia_and_Herzegovina,
        Botswana,
        Brazil,
        Brunei,
        Bulgaria,
        Burundi,
        Cambodia,
        Cameroon,
        Canada,
        [Display(Name = "Central African Republic")]
        Central_African_Republic,
        Chad,
        Chile,
        China,
        Colombia,
        Comoros,
        Congo,
        Costa_Rica,
        Croatia,
        Cuba,
        Cyprus,
        Czech_Republic,
        [Display(Name = "Democratic Republic of the Congo")]
        Democratic_Republic_of_the_Congo,
        Denmark,
        Djibouti,
        Dominica,
        Dominican_Republic,
        Ecuador,
        Egypt,
        El_Salvador,
        Equatorial_Guinea,
        Eritrea,
        Estonia,
        Eswatini,
        Ethiopia,
        Fiji,
        Finland,
        France,
        Gabon,
        Gambia,
        Georgia,
        Germany,
        Ghana,
        Greece,
        Grenada,
        Guatemala,
        Guinea,
        Guyana,
        Haiti,
        Holy_See,
        Honduras,
        Hungary,
        Iceland,
        India,
        Indonesia,
        Iran,
        Iraq,
        Ireland,
        Israel,
        Italy,
        Jamaica,
        Japan,
        Jordan,
        Kazakhstan,
        Kenya,
        Kiribati,
        Kuwait,
        Kyrgyzstan,
        Laos,
        Latvia,
        Lebanon,
        Lesotho,
        Liberia,
        Libya,
        Liechtenstein,
        Lithuania,
        Luxembourg,
        Madagascar,
        Malawi,
        Malaysia,
        Maldives,
        Mali,
        Malta,
        Marshall_Islands,
        Mauritania,
        Mauritius,
        Mexico,
        Micronesia,
        Moldova,
        Monaco,
        Mongolia,
        Montenegro,
        Morocco,
        Mozambique,
        Myanmar,
        Namibia,
        Nauru,
        Nepal,
        Netherlands,
        New_Zealand,
        Nicaragua,
        Niger,
        Nigeria,
        [Display(Name = "North Korea")]
        North_Korea,
        Norway,
        Oman,
        Pakistan,
        Palau,
        Palestine_State,
        Panama,
        Papua_New_Guinea,
        Paraguay,
        Peru,
        Philippines,
        Poland,
        Portugal,
        Qatar,
        Romania,
        Russia,
        Rwanda,
        San_Marino,
        Saudi_Arabia,
        Senegal,
        Serbia,
        Seychelles,
        Singapore,
        Slovakia,
        Slovenia,
        Solomon_Islands,
        Somalia,
        South_Africa,
        [Display(Name = "South Korea")]
        South_Korea,
        South_Sudan,
        Spain,
        Sri_Lanka,
        Sudan,
        Suriname,
        Sweden,
        Switzerland,
        Syria,
        Tajikistan,
        Tanzania,
        Thailand,
        Timor_Leste,
        Togo,
        Tonga,
        Trinidad_and_Tobago,
        Tunisia,
        Turkey,
        Turkmenistan,
        Tuvalu,
        Uganda,
        Ukraine,
        [Display(Name = "United Arab Emirates")]
        United_Arab_Emirates,
        [Display(Name = "United Kindgdom")]
        United_Kingdom,
        Uruguay,
        Uzbekistan,
        Vanuatu,
        Venezuela,
        Vietnam,
        Yemen,
        Zambia,
        Zimbabwe
    }
}
