import Typography from "@mui/material/Typography";

import TabContext from "@mui/lab/TabContext";
import TabList from "@mui/lab/TabList";
import TabPanel from "@mui/lab/TabPanel";

import { useState } from "react";

import Information from "./Information";
import Change from "./Change";

import { PaperStyled, TabStyled } from "../styled";

const tabs = ['Personal information', 'Change password and login']

const Profile = () => {
    const [valueTab, setValueTab] = useState<string>("0");

    const handleChange = (event: React.SyntheticEvent, newValue: string) => {
        setValueTab(newValue);
    };

    return (
        <TabContext value={valueTab} >
            <Typography variant="h1" sx={{ mb: "30px" }}>Settings profile</Typography>
            <TabList
                onChange={handleChange}
                sx={{ px: "33px" }}>
                {tabs.map((item, index) => (
                    <TabStyled
                        key={index}
                        label={item}
                        value={index.toString()}
                    />
                ))}
            </TabList >

            <TabPanel sx={{ p: "0px" }} value="0" >
                <PaperStyled sx={{ padding: "24px 16px 46px", margin: "13px 13px 130px 15px" }}>
                    <Information />
                </PaperStyled>
            </TabPanel>
            <TabPanel sx={{ p: "0px" }} value="1">
                <PaperStyled sx={{ padding: "24px 16px 46px", margin: "13px 13px 130px 15px" }}>
                    <Change />
                </PaperStyled>
            </TabPanel>
        </TabContext>
    )
}

export default Profile;