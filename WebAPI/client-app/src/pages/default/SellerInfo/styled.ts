import Tab from "@mui/material/Tab";
import LinearProgress, { linearProgressClasses } from '@mui/material/LinearProgress';

import { styled } from "@mui/material";

export const TabStyle = styled(Tab)(() => ({
    maxWidth: "none",
    minWidth: "0",
    minHeight: "0",
    fontSize: "20px",
    lineHeight: "25px",
    marginRight: "90px",
    textTransform: "none",
    padding: "0 0 15px 0",
    "&:nth-last": {
        marginRight: "0",
    },
}));

export const BorderLinearProgress = styled(LinearProgress)(({ theme }) => ({
    width: "244px",
    height: "20px",
    borderRadius: "10px",
    [`&.${linearProgressClasses.colorPrimary}`]: {
        backgroundColor: '#D9D9D9',
    },
    [`& .${linearProgressClasses.bar}`]: {
        backgroundColor: theme.palette.primary,
    },
}));
