import { BasketAction, BasketActionTypes, BasketState } from "./types"

const initialState: BasketState = {
    isBasketMenuOpen: false,
    basketItems: [],
}

export const basketReducer = (state = initialState, action: BasketAction): BasketState => {
    switch (action.type) {
        case BasketActionTypes.BASKET_MENU_CHANGE:
            return {
                ...state,
                isBasketMenuOpen: !state.isBasketMenuOpen,
            }
        case BasketActionTypes.GET_BASKET_ITEMS:
            return {
                ...state,
                basketItems: action.payload,
            }
        case BasketActionTypes.RESET_BASKET:
            return {
                ...state,
                basketItems: [],
            }
        default:
            return state;
    }
}