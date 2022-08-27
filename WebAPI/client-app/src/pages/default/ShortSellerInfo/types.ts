export enum ShopInfoActionTypes {
    GET_SHOP_INFO = "GET_SHOP_INFO",
}

export interface IShopInfo {
    photo: string,
    name: string,
    email: string,
    siteUrl: string,
    adress: string,
    phones: Array<string>,
}

export interface ShopInfoState {
    shopInfo: IShopInfo,
}


export interface GetShopInfoAction {
    type: ShopInfoActionTypes.GET_SHOP_INFO,
    payload: IShopInfo
}


export type ShopInfoAction = GetShopInfoAction;