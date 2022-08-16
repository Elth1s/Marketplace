import { TextField, styled } from '@mui/material';

export const TextFieldStyle = styled(TextField)(({ theme }) => ({
    "& .MuiInputLabel-root": {
        padding: "0px",
        color: "#45a29e",
        "&.Mui-focused": {
            color: "#45a29e"
        },
        "&.Mui-error ": {
            color: "#d32f2f"
        },
        "&.Mui-disabled": {
            color: "#f1f1f1"
        },
    },
    "& .MuiOutlinedInput-root": {
        color: "#777",
        "&>*:nth-of-type(1)": {
            padding: "2.5px 8.5px",
            fontSize: "12px",
        },
        "& fieldset": {
            borderRadius: "5px",
            border: "1px solid black"
        },
        "&:hover fieldset": {
            borderRadius: "5px",
            border: "1px solid black"
        },
        "&.Mui-focused fieldset": {
            borderRadius: "5px",
            border: "1px solid black"
        }
    },
}));