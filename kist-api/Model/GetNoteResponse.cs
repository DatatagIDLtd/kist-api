using kist_api.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class GetNoteResponse
    {
        public List<Note> Value { get; set; }
    }
}
