import * as AuthActionCreators from "../../pages/auth/actions"
import * as ProfileActionCreators from "../../pages/user/actions"
import * as CategoryActionCreators from "../../pages/admin/category/actions"
import * as CharacteristicGroupActionCreators from "../../pages/admin/characteristicGroup/actions"
import * as CharacteristicActionCreators from "../../pages/admin/characteristic/actions"

const actions = {
    ...AuthActionCreators,
    ...ProfileActionCreators,
    ...CategoryActionCreators,
    ...CharacteristicGroupActionCreators,
    ...CharacteristicActionCreators,
}
export default actions;