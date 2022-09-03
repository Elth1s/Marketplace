export enum BasketActionTypes {
    GET_BASKET_ITEMS = "GET_BASKET_ITEMS",
    RESET_BASKET = "RESET_BASKET"
}

export interface IBasketItem {
    id: number,
    count: number,
    productName: string,
    productImage: string,
    productPrice: number,
    productCount: number,
    productUrlSlug: string
}

export interface BasketState {
    basketItems: Array<IBasketItem>,
}


export interface GetBasketItemsAction {
    type: BasketActionTypes.GET_BASKET_ITEMS,
    payload: Array<IBasketItem>
}

export interface ResetBasketAction {
    type: BasketActionTypes.RESET_BASKET
}


export type BasketAction = GetBasketItemsAction | ResetBasketAction;