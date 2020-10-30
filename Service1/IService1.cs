using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        DataTable GetData();

        [OperationContract]
        bool Insert(int car_id, int obj_from_id, int obj_to_id, string date_from, string date_to);

        [OperationContract]
        DataTable GetSelectData(string tableName);
    }
}
