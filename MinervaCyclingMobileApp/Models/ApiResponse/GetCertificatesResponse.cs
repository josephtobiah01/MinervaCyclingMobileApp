using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.Models.ApiResponse
{
    public class GetCertificatesResponse : BaseResponse
    {
        public long ContactId { get; set; }

        public string CertificateNumber { get; set; }

        public string Address { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime WarrantyStartDate { get; set; }

        public DateTime WarrantyStopDate { get; set; }

        public string ProductionCode { get; set; }

        public bool IsEbike { get; set; }

        public long WarrantyFormulaId { get; set; }

        public long ProductId { get; set; }

        public long ServiceCenterId { get; set; }

        public string InssuranceNumber { get; set; }

        public string ReplaceBikeInssuranceNumber { get; set; }

        public string BatterySerialnumber { get; set; }

        public string FrameSerialnumber { get; set; }

    }
}
