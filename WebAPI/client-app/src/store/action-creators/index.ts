import * as AuthActionCreators from "../../components/auth/actions"
import * as ProfileActionCreators from "../../components/user/Profile/actions"
import * as CategoryActionCreators from "../../components/category/actions"
import * as CharacteristicGroupActionCreators from "../../views/characteristicGroup/actions"

const actions = {
    ...AuthActionCreators,
    ...ProfileActionCreators,
    ...CategoryActionCreators,
    ...CharacteristicGroupActionCreators,
}
export default actions;