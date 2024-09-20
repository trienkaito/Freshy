namespace FRESHY.SharedKernel.Interfaces;

public interface IQrService
{
    byte[] GenerateQrCode(Guid orderId);    
}