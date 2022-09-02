import Box from '@mui/material/Box';
import AccordionDetails from '@mui/material/AccordionDetails';
import Typography from '@mui/material/Typography';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

import { useTranslation } from 'react-i18next';
import { FC, useState } from 'react';

import { AccordionStyle, AccordionSummaryStyle, TabsStyle, TabStyle } from './styled';

import { info_45, package_45, shopping_bag_45, truck_45 } from '../../../../assets/icons';

interface TabPanelProps {
    children?: React.ReactNode;
    index: number;
    value: number;
}

interface IAccordionItem {
    question: string;
    answer: string;
}

function TabPanel(props: TabPanelProps) {
    const { children, value, index, ...other } = props;

    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`simple-tabpanel-${index}`}
            aria-labelledby={`simple-tab-${index}`}
            {...other}
        >
            {value === index && (
                <Box
                    sx={{
                        display: "flex",
                        flexDirection: "column",
                        justifyContent: "center",
                        alignItems: "center",
                        mt: "50px"
                    }}>
                    {children}
                </Box>
            )}
        </div>
    );
}

const AccordionItem: FC<IAccordionItem> = ({ question, answer }) => {
    return (
        <AccordionStyle>
            <AccordionSummaryStyle
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel1a-content"
                id="panel1a-header"
            >
                <Typography variant="h5">{question}</Typography>
            </AccordionSummaryStyle>
            <AccordionDetails sx={{ padding: "30px 0px 10px" }}>
                <Typography variant="h5">
                    {answer}
                </Typography>
            </AccordionDetails>
        </AccordionStyle>
    )
}

function a11yProps(index: number) {
    return {
        id: `simple-tab-${index}`,
        'aria-controls': `simple-tabpanel-${index}`,
    };
}

const faqMain = [
    { question: "Sodales ut etiam sit amet", answer: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Dignissim cras tincidunt lobortis feugiat. Adipiscing elit duis tristique sollicitudin. Vel fringilla est ullamcorper eget nulla facilisi. Interdum velit euismod in pellentesque massa placerat duis. Ipsum dolor sit amet consectetur adipiscing elit duis. Nisi scelerisque eu ultrices vitae auctor eu augue ut. Convallis posuere morbi leo urna molestie at. Accumsan in nisl nisi scelerisque eu ultrices. Mi eget mauris pharetra et ultrices neque ornare aenean euismod." },
    { question: "Non odio euismod lacinia at quis risus sed", answer: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Platea dictumst quisque sagittis purus sit amet volutpat consequat. Ut diam quam nulla porttitor massa id neque aliquam." },
    { question: "Etiam erat velit scelerisque in dictum non consectetur a erat nam at", answer: "Integer feugiat scelerisque varius morbi enim nunc. Aenean vel elit scelerisque mauris pellentesque. Netus et malesuada fames ac turpis egestas maecenas. Etiam sit amet nisl purus in. Integer eget aliquet nibh praesent tristique magna sit. Vitae suscipit tellus mauris a diam maecenas sed enim ut." },
    { question: "Porttitor massa id neque aliquam vestibulum", answer: "Nisi quis eleifend quam adipiscing vitae proin sagittis nisl. Duis tristique sollicitudin nibh sit amet." },
]

const faqOrder = [
    { question: "Dolor sit amet consectetur adipiscing elit duis tristique sollicitudin.", answer: "Sed elementum tempus egestas sed sed risus. Dolor sit amet consectetur adipiscing elit ut aliquam purus. Ultricies mi quis hendrerit dolor magna eget est lorem ipsum. Imperdiet dui accumsan sit amet nulla facilisi morbi. Dui id ornare arcu odio ut. Sed euismod nisi porta lorem." },
    { question: "Phasellus faucibus scelerisque eleifend donec pretium.", answer: "Elit ullamcorper dignissim cras tincidunt lobortis. Sed arcu non odio euismod lacinia at. Nunc congue nisi vitae suscipit tellus mauris a diam." },
    { question: "Ante metus dictum at tempor commodo ullamcorper a. Porta lorem mollis aliquam ut porttitor leo.", answer: "Eget arcu dictum varius duis. In fermentum posuere urna nec. Tortor aliquam nulla facilisi cras. Mi in nulla posuere sollicitudin aliquam ultrices sagittis orci. Sit amet cursus sit amet dictum sit amet." },
]

const faqDeliver = [
    { question: "Etiam tempor orci eu lobortis elementum nibh. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.", answer: "Hac habitasse platea dictumst vestibulum rhoncus est pellentesque. Lobortis scelerisque fermentum dui faucibus in ornare. Ornare massa eget egestas purus viverra accumsan in nisl nisi. Maecenas pharetra convallis posuere morbi leo urna. Vestibulum morbi blandit cursus risus at." },
    { question: "In massa tempor nec feugiat nisl pretium fusce id.", answer: "Pulvinar elementum integer enim neque volutpat ac tincidunt. Pretium viverra suspendisse potenti nullam ac tortor vitae purus faucibus. Magna sit amet purus gravida quis blandit turpis." },
    { question: "Diam quis enim lobortis scelerisque fermentum.", answer: "Egestas sed tempus urna et pharetra pharetra. Maecenas pharetra convallis posuere morbi leo. Ultricies integer quis auctor elit." },
]

const faqReturn = [
    { question: "Auctor urna nunc id cursus metus aliquam eleifend mi in.", answer: "Luctus venenatis lectus magna fringilla urna porttitor. Vitae ultricies leo integer malesuada nunc vel risus. Nunc congue nisi vitae suscipit tellus." },
    { question: "Aenean euismod elementum nisi quis eleifend quam adipiscing", answer: "Integer malesuada nunc vel risus commodo viverra. Nunc scelerisque viverra mauris in aliquam sem fringilla ut morbi. Vel pharetra vel turpis nunc eget lorem." },
    { question: "Porta nibh venenatis cras sed felis.", answer: "In dictum non consectetur a erat nam at. At in tellus integer feugiat. Tempor orci dapibus ultrices in iaculis nunc sed. Dictum fusce ut placerat orci nulla pellentesque dignissim enim." },
    { question: "Suscipit adipiscing bibendum est ultricies integer quis.", answer: "Lectus nulla at volutpat diam ut venenatis tellus. Orci eu lobortis elementum nibh tellus. Vel quam elementum pulvinar etiam non quam lacus suspendisse faucibus." },
]

const FAQ = () => {
    const { t } = useTranslation();
    const [value, setValue] = useState(0);

    const handleChange = (event: React.SyntheticEvent, newValue: number) => {
        setValue(newValue);
    };

    return (
        <>
            <TabsStyle value={value} onChange={handleChange} sx={{ minHeight: "0" }} >
                <TabStyle icon={<img src={info_45} alt="icon info" />} label={t('pages.faq.tabs.main')} {...a11yProps(0)} />
                <TabStyle icon={<img src={shopping_bag_45} alt="icon info" />} label={t('pages.faq.tabs.order')} {...a11yProps(1)} />
                <TabStyle icon={<img src={truck_45} alt="icon shopping bag" />} label={t('pages.faq.tabs.deliver')} {...a11yProps(2)} />
                <TabStyle icon={<img src={package_45} alt="icon package" />} label={t('pages.faq.tabs.return')} {...a11yProps(3)} />
            </TabsStyle>

            <TabPanel value={value} index={0}>
                {faqMain.map((item, index) => (
                    <AccordionItem key={index} question={item.question} answer={item.answer} />
                ))}
            </TabPanel>
            <TabPanel value={value} index={1}>
                {faqOrder.map((item, index) => (
                    <AccordionItem key={index} question={item.question} answer={item.answer} />
                ))}
            </TabPanel>
            <TabPanel value={value} index={2}>
                {faqDeliver.map((item, index) => (
                    <AccordionItem key={index} question={item.question} answer={item.answer} />
                ))}
            </TabPanel>
            <TabPanel value={value} index={3}>
                {faqReturn.map((item, index) => (
                    <AccordionItem key={index} question={item.question} answer={item.answer} />
                ))}
            </TabPanel>
        </>
    );
}

export default FAQ;