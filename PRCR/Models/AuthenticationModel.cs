using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PRCR.Models
{
    public class AuthenticationModel
    {
        public string EmpCode { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateUserModel
    {
        public AuthenticateUserModel(){
            USERROLEMAPPING = new UserRoleMappingModel();
        }

        public string PERNR { get; set; }
        public string ENAME { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string EGRP { get; set; }
        public string ESGRP { get; set; }
        public string CATG { get; set; }
        public string PRSAREA { get; set; }
        public string PRSSUBAREA { get; set; }
        public string LOCATION { get; set; }
        public string DESIG { get; set; }
        public string JOB { get; set; }
        public string DEPT { get; set; }
        public string NATIO { get; set; }
        public string RELIGION { get; set; }
        public DateTime? DOJ { get; set; }
        public DateTime? DOR { get; set; }
        public DateTime? DOC { get; set; }
        public DateTime? DOL { get; set; }
        public string MARITAL { get; set; }
        public string GENDER { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public string ICNUM { get; set; }
        public string BANKL { get; set; }
        public string BANKN { get; set; }
        public string PMODE { get; set; }
        public string TSTID { get; set; }
        public string PENID { get; set; }
        public string EEPFN { get; set; }
        public string EEPNN { get; set; }
        public string EEVPF { get; set; }
        public string EVPFA { get; set; }
        public string EMPPFBAS { get; set; }
        public string ERPFBAS { get; set; }
        public string PNFLG { get; set; }
        public string PENBAS { get; set; }
        public string SUP { get; set; }
        public string RECSTAT { get; set; }
        public string LINKCD { get; set; }
        public DateTime? LAST_PROM { get; set; }
        public string STELL { get; set; }
        public string ORGEH { get; set; }
        public string FANAME { get; set; }
        public DateTime? DOJJK { get; set; }
        public string DIVISION { get; set; }
        public string QUAL { get; set; }
        public string LOC_CD { get; set; }
        public string SAREA_CD { get; set; }
        public string USERTYPE { get; set; }
        public UserRoleMappingModel USERROLEMAPPING { get; set; }
    }
}