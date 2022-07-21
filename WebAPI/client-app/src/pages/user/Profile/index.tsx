import Tab from "@mui/material/Tab";
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
            <TabList onChange={handleChange}>
                {tabs.map((item, index) => (
                    <TabStyled
                        key={index}
                        label={item}
                        value={index.toString()}
                    />
                ))}
            </TabList >

            <TabPanel sx={{ p: "0px" }} value="0" >
                <PaperStyled>
                    <Information />
                </PaperStyled>
            </TabPanel>
            <TabPanel sx={{ p: "0px" }} value="1">
                <PaperStyled>
                    <Change />
                </PaperStyled>
            </TabPanel>
        </TabContext>
    )
}

export default Profile;