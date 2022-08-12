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
    "& .MuiInput-underline": {
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

export const TextFieldFirstStyle = styled(TextField)<TextFieldProps>(({ theme }) => ({
    "& .MuiInputLabel-root": {
        color: "#000",
        "&.Mui-focused": {
            color: theme.palette.secondary.main
        },
        "&.Mui-error ": {
            color: theme.palette.error.main
        },
    },
    "& .MuiInput-underline:before": {
        borderBottom: "2px solid #000"
    },
    "& .MuiInput-underline:after": {
        borderBottom: `2px solid ${theme.palette.secondary.main}`
    },
    "& .MuiInput-underline:hover:before": {
        borderBottom: "2px solid blue"
    },
    "& .MuiIconButton-root": {
        color: "#000",
    },
}));

export const TextFieldSecondStyle = styled(TextField)<TextFieldProps>(({ theme }) => ({
    "& .MuiInputLabel-root": {
        color: "black",
        "&.Mui-focused": {
            color: "black"
        },
        "&.Mui-error ": {
            color: `${theme.palette.error.main}`
        },
        "&.Mui-disabled": {
            color: "#777777"
        },
    },
    "& .MuiOutlinedInput-root": {
        color: "black",
        "& fieldset": {
            borderColor: "black"
        },
        "&:hover fieldset": {
            borderColor: "black"
        },
        "&.Mui-focused fieldset": {
            borderColor: "black"
        },
        "&.Mui-disabled fieldset": {
            borderColor: "#7e7e7e"
        },
        "&.Mui-error fieldset": {
            borderColor: `${theme.palette.error.main}`
        },
        "&.Mui-error:hover fieldset": {
            borderColor: `${theme.palette.error.main}`
        }
    },
}));