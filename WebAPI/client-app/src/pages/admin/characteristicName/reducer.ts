import {
    CharacteristicNameAction,
    CharacteristicNameActionTypes,
    CharacteristicNameState
} from "./types";

const initialState: CharacteristicNameState = {
    characteristicNameInfo: {
        id: 0,
        name: "",
        characteristicGroupName: "",
        unitMeasure: "",
    },
    count: 0,
    characteristicNames: []
}

export const characteristicNameReducer = (state = initialState, action: CharacteristicNameAction): CharacteristicNameState => {
    switch (action.type) {
        case CharacteristicNameActionTypes.GET_CHARACTERISTIC_NAMES:
            return {
                ...state,
                characteristicNames: action.payload,
            }
            case CharacteristicNameActionTypes.SEARCH_CHARACTERISTIC_NAMES:
            return {
                ...state,
                count: action.payload.count,
                characteristicNames: action.payload.characteristicNames,
            }
        case CharacteristicNameActionTypes.GET_BY_ID_CHARACTERISTIC_NAME:
            return {
                ...state,
                characteristicNameInfo: action.payload,
            }
        case CharacteristicNameActionTypes.CREATE_CHARACTERISTIC_NAME:
            return {
                ...state,
                ...action.payload
            }
        case CharacteristicNameActionTypes.UPDATE_CHARACTERISTIC_NAME:
            return {
                ...state,
                ...action.payload
            }
        default:
            return state;
    }
}