import {
    CharacteristicNameAction,
    CharacteristicNameActionTypes,
    CharacteristicNameState
} from "./types";

const initialState: CharacteristicNameState = {
    selectedCharacteristicName: {
        name: "",
        characteristicGroupId: 0,
        unitId: 0,
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
                characteristicNames: action.payload.values,
            }
        case CharacteristicNameActionTypes.GET_BY_ID_CHARACTERISTIC_NAME:
            return {
                ...state,
                selectedCharacteristicName: action.payload,
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