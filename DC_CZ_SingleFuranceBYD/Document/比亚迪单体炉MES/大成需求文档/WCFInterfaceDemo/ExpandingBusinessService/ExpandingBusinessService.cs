//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------



[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IExpandingBusinessService")]
public interface IExpandingBusinessService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpandingBusinessService/PassStationCheck", ReplyAction="http://tempuri.org/IExpandingBusinessService/PassStationCheckResponse")]
    string PassStationCheck(string Account, string BarCode, string ActionID, string CheckType, string TerminalID, string EquipmentID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpandingBusinessService/PassStationCheck", ReplyAction="http://tempuri.org/IExpandingBusinessService/PassStationCheckResponse")]
    System.Threading.Tasks.Task<string> PassStationCheckAsync(string Account, string BarCode, string ActionID, string CheckType, string TerminalID, string EquipmentID);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IExpandingBusinessServiceChannel : IExpandingBusinessService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class ExpandingBusinessServiceClient : System.ServiceModel.ClientBase<IExpandingBusinessService>, IExpandingBusinessService
{
    
    public ExpandingBusinessServiceClient()
    {
    }
    
    public ExpandingBusinessServiceClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public ExpandingBusinessServiceClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public ExpandingBusinessServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public ExpandingBusinessServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public string PassStationCheck(string Account, string BarCode, string ActionID, string CheckType, string TerminalID, string EquipmentID)
    {
        return base.Channel.PassStationCheck(Account, BarCode, ActionID, CheckType, TerminalID, EquipmentID);
    }
    
    public System.Threading.Tasks.Task<string> PassStationCheckAsync(string Account, string BarCode, string ActionID, string CheckType, string TerminalID, string EquipmentID)
    {
        return base.Channel.PassStationCheckAsync(Account, BarCode, ActionID, CheckType, TerminalID, EquipmentID);
    }
}
