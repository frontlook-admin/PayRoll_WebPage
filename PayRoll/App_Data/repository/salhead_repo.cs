using System;
using System.Diagnostics.CodeAnalysis;

namespace PayRoll.App_Data.repository
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Salhead_repo
    {
        public string _code, _name, _groupcode, _formula, _oldname;
        public DateTime _startdate;
        public int _id;
        public bool _add_to_salinfo;

        public Salhead_repo()
        {
            
        }

        public Salhead_repo(string code, string name,  string groupcode, string formula,bool add_to_salinfo, DateTime startdate)
        {
            this.Code = code;
            this.Name = name;
            this.Add_To_SalInfo = add_to_salinfo;
            this.GroupCode = groupcode;
            this.Formula = formula;
            this.Startdate = startdate;
        }

        public Salhead_repo(int id, string code, string name,  string groupcode, string formula, bool add_to_salinfo, DateTime startdate)
        {
            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.Add_To_SalInfo = add_to_salinfo;
            this.GroupCode = groupcode;
            this.Formula = formula;
            this.Startdate = startdate;
        }

        public Salhead_repo(int id, string code, string name,  string groupcode, string formula, bool add_to_salinfo, DateTime startdate, string oldname)
        {
            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.Add_To_SalInfo = add_to_salinfo;
            this.Formula = formula;
            this.GroupCode = groupcode;
            this.Oldname = oldname;
            this.Startdate = startdate;
        }


        public string Oldname
        {
            get => _oldname;
            set => _oldname = value;
        }

        public string GroupCode
        {
            get => _groupcode;
            set => _groupcode = value;
        }
        public string Formula
        {
            get => _formula;
            set => _formula = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Code
        {
            get => _code;
            set => _code = value;
        }

        public bool Add_To_SalInfo
        {
            get => _add_to_salinfo;
            set => _add_to_salinfo = value;
        }

        public DateTime Startdate
        {
            get => _startdate;
            set => _startdate = value;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }
    }
}
