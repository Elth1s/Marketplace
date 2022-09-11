import MenuItem from "@mui/material/MenuItem";
import { TextFieldFirstStyle } from "../TextField/styled";

import { ISelect } from "./type";

const SelectComponent: React.FC<ISelect> = ({
    label,
    touched,
    error,
    getFieldProps,
    items
}) => {
    return (
        <TextFieldFirstStyle
            select
            fullWidth
            variant="standard"
            label={label}
            error={Boolean(touched && error)}
            helperText={touched && error}
            {...getFieldProps}
        >
            {items && items.map((item) =>
                <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
            )}
        </TextFieldFirstStyle>
    )
}

export default SelectComponent;