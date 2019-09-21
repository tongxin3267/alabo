﻿using System.Collections.Generic;

namespace Alabo.App.Core.Finance.Domain.Dtos.BankCard {

    public class AliResult {
        public string CardType { get; set; }

        public string Bank { get; set; }

        public string Key { get; set; }

        public List<string> Messages { get; set; }

        public string Validated { get; set; }

        public string Stat { get; set; }
    }
}