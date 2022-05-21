import {
    CharacteristicGroupAction,
    CharacteristicGroupActionTypes,
    CharacteristicGroupState
} from "./types";

const initialState: CharacteristicGroupState = {
    characteristicGroupInfo: {
        id: 0,
        name: "",
    },
    characteristicGroups: []
}

export const characteristicGroupReducer = (state = initialState, action: CharacteristicGroupAction): CharacteristicGroupState => {
    switch (action.type) {
        case CharacteristicGroupActionTypes.GET_CHARACTERISTIC_GROUPS:
            return {
                ...state,
                characteristicGroups: action.payload,
            }
        case CharacteristicGroupActionTypes.GET_BY_ID_CHARACTERISTIC_GROUP:
            return {
                ...state,
                characteristicGroupInfo: action.payload,
            }
        case CharacteristicGroupActionTypes.CREATE_CHARACTERISTIC_GROUP:
            return {
                ...state,
                ...action.payload
            }
        case CharacteristicGroupActionTypes.UPDATE_CHARACTERISTIC_GROUP:
            return {
                ...state,
                ...action.payload
            }
        default:
            return state;
    }
}