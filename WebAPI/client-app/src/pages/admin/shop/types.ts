export enum ShopActionTypes {
    GET_SHOPS = "GET_SHOPS",
    GET_BY_ID_SHOP = "GET_BY_ID_SHOP",
    SEARCH_SHOPS = "SEARCH_SHOPS"
}

export interface IShop {
    name: string,
    description: string,
    email: string,
    phones: Array<string>,
    photo: string,
    siteUrl: string,
    countryName: string,
    cityName: string,
    userFullName: string
}

export interface IShopInfo {
    id: number,
    name: string,
    photo: string,
    siteUrl: string,
    cityName: string
}

export interface ISearchShops {
    count: number,
    values: Array<IShopInfo>
}

export interface ShopState {
    selectedShop: IShop,
    count: number,
    shops: Array<IShopInfo>
}


export interface GetShopsAction {
    type: ShopActionTypes.GET_SHOPS,
    payload: Array<IShopInfo>
}

export interface SearchShopsAction {
    type: ShopActionTypes.SEARCH_SHOPS,
    payload: ISearchShops
}

export interface GetByIdShopAction {
    type: ShopActionTypes.GET_BY_ID_SHOP,
    payload: IShop
}


export type ShopAction = GetShopsAction |
    SearchShopsAction |
    GetByIdShopAction 