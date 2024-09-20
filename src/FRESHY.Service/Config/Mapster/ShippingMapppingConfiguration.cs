using FRESHY.Main.Application.Abstractions.ShippingAbstractions.Commands.CreateNewShippingPartner;
using FRESHY.Main.Application.Abstractions.ShippingAbstractions.Queries.GetAllShippingPartnersDetails.Results;
using FRESHY.Main.Contract.Requests.ShippingRequests;
using FRESHY.Main.Contract.Responses.ShippingResponses;
using Mapster;

namespace FRESHY.Service.Config.Mapster;

public class ShippingMapppingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Requests Mapping
        config.NewConfig<CreateNewShippingPartnerRequest, CreateNewShippingPartnerCommand>();

        //Requests Mapping
        config.NewConfig<AllShippingPartnersResult, AllShippingPartersResponse>();
    }
}