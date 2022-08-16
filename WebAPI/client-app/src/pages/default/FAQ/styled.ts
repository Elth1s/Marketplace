import Tabs from "@mui/material/Tabs";
import Tab from "@mui/material/Tab";
import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';

import { styled } from "@mui/material";

export const TabsStyle = styled(Tabs)(() => ({
    "& .MuiTabs-flexContainer": {
        justifyContent: "space-between",
    },
    "& .MuiTabs-indicator": {
        display: "none",
    }
}));

export const TabStyle = styled(Tab)(() => ({
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    width: "355px",
    height: "220px",
    color: "#000000",
    fontSize: "18px",
    lineHeight: "23px",
    fontWeight: "500",
    border: "1px solid #7E7E7E",
    borderRadius: "10px",
    "&>.MuiTab-iconWrapper": {
        marginBottom: "15px",
    },
    "&.Mui-selected": {
        color: "#000000",
        background: "#DFDFDF",
    }
}));

export const AccordionStyle = styled(Accordion)(() => ({
    "&.MuiAccordion-root": {
        width: "1030px",
        marginBottom: "20px",
        boxShadow: "none",
        "&:before": {
            content: "none",
        },
        "&.Mui-expanded": {
            margin: "0 0 20px",
        },
        "&:first-of-type": {
            borderRadius: "10px",
        },
        "&:last-of-type": {
            borderRadius: "10px",
            marginBottom: "0",
        }
    }
}));

export const AccordionSummaryStyle = styled(AccordionSummary)(() => ({
    border: "1px solid #7E7E7E",
    borderRadius: "10px",
    padding: "0px 15px",
    "&.Mui-expanded": {
        minHeight: "48px",
    },
    ".MuiAccordionSummary-content": {
        margin: "20px 0",
    }
}));