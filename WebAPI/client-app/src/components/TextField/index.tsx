import { TextFieldStyle } from "./style";
import { ITextField } from "./type";

const TextFieldComponent: React.FC<ITextField> =({ label, touched, error, type, getFieldProps }) => {
    return (
        <TextFieldStyle
            fullWidth
            variant="standard"
            type={type}
            label={label}
            error={Boolean(touched && error)}
            helperText={touched && error}
            {...getFieldProps}
        />
    )
}

export default TextFieldComponent;
