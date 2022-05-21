import {
    CharacteristicAction,
    CharacteristicActionTypes,
    CharacteristicState
} from "./types";

const initialState: CharacteristicState = {
    characteristicInfo: {
        id: 0,
        name: "",
        characteristicGroupName: "",
    },
    characteristics: []
}

export const characteristicReducer = (state = initialState, action: CharacteristicAction): CharacteristicState => {
    switch (action.type) {
        case CharacteristicActionTypes.GET_CHARACTERISTICS:
            return {
                ...state,
                characteristics: action.payload,
            }
        case CharacteristicActionTypes.GET_BY_ID_CHARACTERISTIC:
            return {
                ...state,
                characteristicInfo: action.payload,
            }
        case CharacteristicActionTypes.CREATE_CHARACTERISTIC:
            return {
                ...state,
                ...action.payload
            }
        case CharacteristicActionTypes.UPDATE_CHARACTERISTIC:
            return {
                ...state,
                ...action.payload
            }
        default:
            return state;
    }
}