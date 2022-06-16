import TextField, { TextFieldProps } from '@mui/material/TextField';
import { styled } from '@mui/material/styles';

export const TextFieldStyle = styled(TextField)<TextFieldProps>(({ theme }) => ({
    "& .MuiInputLabel-root": {
        color: "#000",
        "&.Mui-focused": {
            color: theme.palette.secondary.main
        },
        "&.Mui-error ": {
            color: theme.palette.error.main
        },
    },
    "& .MuiInput-underline":{
        ":before": {
            borderBottom: "2px solid #000"
        },
        ":after": {
            borderBottom: `2px solid ${theme.palette.secondary.main}`
        },
    },
    "& .MuiIconButton-root": {
        color: "#000",
    },
}));