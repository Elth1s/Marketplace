import {
    CharacteristicValueAction,
    CharacteristicValueActionTypes,
    CharacteristicValueState
} from "./types";

const initialState: CharacteristicValueState = {
    selectedCharacteristicValue: {
        value: "",
        characteristicNameId: 0,
    },
    count: 0,
    characteristicValues: []
}

export const characteristicValueReducer = (state = initialState, action: CharacteristicValueAction): CharacteristicValueState => {
    switch (action.type) {
        case CharacteristicValueActionTypes.GET_CHARACTERISTIC_VALUES:
            return {
                ...state,
                characteristicValues: action.payload,
            }
        case CharacteristicValueActionTypes.SEARCH_CHARACTERISTIC_VALUE:
            return {
                ...state,
                count: action.payload.count,
                characteristicValues: action.payload.values,
            }
        case CharacteristicValueActionTypes.GET_BY_ID_CHARACTERISTIC_VALUE:
            return {
                ...state,
                selectedCharacteristicValue: action.payload,
            }
        case CharacteristicValueActionTypes.CREATE_CHARACTERISTIC_VALUE:
            return {
                ...state,
                ...action.payload
            }
        case CharacteristicValueActionTypes.UPDATE_CHARACTERISTIC_VALUE:
            return {
                ...state,
                ...action.payload
            }
        default:
            return state;
    }
}