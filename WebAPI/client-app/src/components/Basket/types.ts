export enum BasketActionTypes {
    BASKET_MENU_CHANGE = "BASKET_MENU_CHANGE",
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
    isBasketMenuOpen: boolean,
    basketItems: Array<IBasketItem>,
}

export interface BasketDialogChangeAction {
    type: BasketActionTypes.BASKET_MENU_CHANGE
}

export interface GetBasketItemsAction {
    type: BasketActionTypes.GET_BASKET_ITEMS,
    payload: Array<IBasketItem>
}

export interface ResetBasketAction {
    type: BasketActionTypes.RESET_BASKET
}


export type BasketAction = BasketDialogChangeAction | GetBasketItemsAction | ResetBasketAction;