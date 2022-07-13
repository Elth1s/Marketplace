import { BasketAction, BasketActionTypes, BasketState } from "./types"

const initialState: BasketState = {
    basketItems: [],
}

export const basketReducer = (state = initialState, action: BasketAction): BasketState => {
    switch (action.type) {
        case BasketActionTypes.GET_BASKET_ITEMS:
            return {
                ...state,
                basketItems: action.payload,
            }
        default:
            return state;
    }
}