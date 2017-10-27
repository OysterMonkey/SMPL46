using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPL46.Services
{
    public interface IPropertyService
    {
        string ReadProperty(string key);

        bool HasProperty(string key);
    }
}
