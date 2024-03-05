  --------------------------- -------------------------------------------
                              

  --------------------------- -------------------------------------------

> **Serious Game Development for chronic disease management**
>
> by
>
> Alexandros Filios

Submitted

in partial fulfilment of the requirements for the degree of

Master of Artificial Intelligence

at the

UNIVERSITY OF PIRAEUS

February 2024

University of Piraeus, NCSR "Demokritos". All rights reserved.

Author: Alexandros Filios

Thesis Supervisor: Ilias Magklogiannis

Title: Professor

> II-MSc "Artificial Intelligence"
>
> Feb 10, 2024

-   Certified by Ilias Magklogiannis, Professor, Thesis Supervisor.

    > Certified by Theodoros Giannakopoulos, Professor, Member of
    > Examination Committee.

    > Certified by Michael Filippakis, Professor, Member of Examination
    > Committee.

**Serious Game Development for chronic disease management**

**By**

**Alexandros Filios**

> Submitted to the II-MSc "Artificial Intelligence" on February 10,
> 2024, in partial fulfillment of the
>
> requirements for the MSc degree
>
> **Abstract**
>
> This master\'s thesis focuses on the development of a Serious Game,
> named Avitar, tailored for chronic disease management through an
> interactive approach. Traditional health apps often fail to engage
> users due to their perceived monotony. Avitar addresses this challenge
> by utilizing Unity and HealthKit API to create a visually appealing
> environment where an Avatar serves as a dynamic representation of the
> user\'s health status.

The game incorporates data from the HealthKit App, including
heart-related metrics (heart rate, high/low heart rate events, irregular
rhythm events), activity data (steps, distance walking/running), and
vitals (blood glucose, insulin deliveries) over the past 24 hours.
Avitar acts as a personalized guide, offering simple tips derived from
this data to motivate users without being overly prescriptive.

The methodology involves two key components: efficient data retrieval
and structuring from HealthKit and the aesthetic aspect of creating the
Avatar using UModeler X library. The InWorld UNITY SDK breathes life
into Avitar, enabling dynamic interaction with the user\'s health data.

While the emphasis lies on implementation, the thesis acknowledges the
need for further research in refining the user experience. The game\'s
success is measured not only by its technical prowess but also by its
ability to provide a satisfying and motivating digital health
experience, addressing a gap in the current landscape of health apps.

**Acknowledgments**

> Any opinions, findings, conclusions or recommendations expressed in
> this material are those of the author(s) and do not necessarily
> reflect the views of the «funding body» or the view of University of
> Piraeus and Inst. of Informatics and Telecom. of NCSR "Demokritos".
>
> We extend our gratitude to UModeler, Inc, the creators of UModeler X,
> for their exceptional Unity SDK which significantly contributed to the
> development of this project. The versatile and robust features of
> UModeler X have enabled a seamless and efficient design process,
> underpinning the creation of immersive and interactive environments
> crucial for the success of this serious game aimed at chronic disease
> management
>
> We really appreciate also the support by Inworld and their friendly
> and helpful community, as well as their kind and motivating words:
>
> *\"Inworld AI is proud to support and enable academics in exploring
> how immersive characters can positively affect the end user
> experience, especially for Serious Games and Simulations. We will
> continue to improve our platform to make integrating our AI characters
> seamless and easy to customize."*

**Contents**

￼

**List of Figures**

￼

**List of Table**

**￼**

1.  **Introduction**

The intersection of artificial intelligence and healthcare has opened
new avenues for innovative solutions. In this context, the development
of a Serious Game, named Avitar, emerges as a novel approach for chronic
disease management. Avitar seeks to bridge the gap between conventional
health apps and user engagement by creating an interactive and visually
immersive experience.

1.  1.  **Background and Motivation**

Within the landscape of health applications, a pervasive challenge lies
in their recurring difficulty to captivate and maintain user interest.
This widespread disengagement stems from a predominant perception of
these apps as tedious chores rather than immersive experiences. The crux
of this issue is frequently traced back to an unbalanced emphasis on
technical and utility aspects, often to the detriment of the User
Interface (UI) design. In this context, the UI serves as the gateway to
user interaction and experience, yet its significance is often
overshadowed by a disproportionate focus on functionality.

Moreover, the disconnect between users and health applications can be
further attributed to suboptimal data handling practices. Frequently,
these apps present vital health information in a raw and convoluted
manner, manifesting as inscrutable charts and numbers that lack
meaningful context. This visual complexity not only contributes to user
disinterest but also perpetuates misunderstandings. Users, grappling
with this overwhelming data presentation, may inadvertently divert their
attention towards metrics of lesser consequence, potentially neglecting
critical health indicators in the process.

The confluence of these factors creates a substantial gap between users
and their health data, hindering the potential for informed
decision-making and proactive health management. As this gap widens, the
efficacy of health applications as tools for positive behavior change
and sustained engagement diminishes. Recognizing these challenges
underscores the necessity for innovative solutions that transcend the
conventional paradigms of health app design.

In response to this challenge, this master\'s thesis introduces Avitar,
an innovative game designed to transcend the limitations of conventional
health apps. Avitar not only monitors health metrics using the HealthKit
API but also transcends raw data by translating it into a visually
representative Avatar. The motivation behind Avitar arises from the
imperative to provide users with a more engaging and enjoyable avenue
for health management, with the ultimate goal of cultivating lasting
commitment and positive behavioral changes. As technological
advancements continue to unfold, the amalgamation of serious gaming and
health monitoring emerges as a critical frontier, compelling exploration
and innovation within this interdisciplinary domain.

The integration of personalization in the in-game character and
environment, driven by user choices or behavioral patterns monitored in
the background, aligns with established gaming practices. Drawing
inspiration from games employing Non-Playable Characters (NPCs) that
dynamically interact based on player decisions, this approach not only
enhances re-playability and realism but also intensifies the user\'s
engagement. Extending this strategy to medical applications represents a
significant stride, leveraging the potential of Learning-Based Models
(LLMs) to further enhance user experiences in health management.

1.  1.  **Problem Statement**

As of today, a conspicuous void persists in the form of an absence of a
truly innovative platform that seamlessly translates the intricacies of
an individual\'s health into an aesthetically pleasing and interactive
experience, utilizing the capabilities of Artificial Intelligence (AI).
Despite the proliferation of health apps, there is a marked dearth of
successful applications that harness the power of AI to create a dynamic
Avatar reflecting the user\'s health status. This visionary gap not only
limits the potential for personalized health management but also
overlooks the transformative impact such an application could have on
user engagement and long-term commitment.

Furthermore, the untapped potential of leveraging HealthKit data
presents a pressing issue. The existing health apps largely fail to
explore the full spectrum of possibilities offered by HealthKit, merely
scratching the surface in terms of data utilization. This limitation
represents a substantial opportunity cost, hindering the development of
applications that could optimize health data from unique perspectives,
providing users with valuable insights beyond the conventional health
metrics. The untapped reservoir of HealthKit\'s potential, if unlocked,
could revolutionize the paradigm of health app functionality.

Beyond the technical aspects, a significant societal challenge arises
from the lack of user-friendly and diverting solutions for health
management. The conventional approach of relying on notes, reminders,
and complex health apps often results in fragmented, inconsistent
tracking, particularly among the elderly population. Additionally,
children grappling with chronic conditions, such as diabetes, face the
arduous task of consistent insulin deliveries, often detesting the
monotonous chore. The absence of an engaging diversionary tool
exacerbates these challenges, highlighting the need for a visionary
health application that transcends the conventional to address diverse
user demographics and their unique health management needs.

Moreover, the prevalent complexity of health apps, dominated by charts,
numbers, and tables, poses a universal hindrance to user engagement. A
considerable portion of the population may not possess a proficient
understanding of mathematical representations, making these apps
inaccessible and discouraging for users with varying levels of numeracy.
This accessibility gap underscores the necessity for an inclusive and
visionary health application that transcends the limitations of
conventional data representation methods, ensuring that health
management remains intuitive and universally accessible.

A prominent challenge lies in the prevailing disengagement caused by the
lack of a gamified and interactive approach to health management.
Current health applications often struggle to sustain user interest,
resulting in sporadic usage and limited effectiveness in fostering
positive behavior change. The absence of an engaging and immersive
platform contributes to a pervasive sense of apathy towards health
monitoring, hindering users from actively participating in their
well-being. Addressing this issue requires a visionary solution that not
only captures user attention but also transforms health management into
a dynamic and enjoyable experience, encouraging sustained interaction
and fostering a sense of personal accomplishment.

Additionally, the current implementations lack a solution that
seamlessly integrates personalized guidance based on individual health
metrics. While data tracking is a common feature, the absence of
tailored insights and recommendations diminishes the utility of these
applications. Users are often left to interpret their health data in
isolation, without clear guidance on how to translate these metrics into
actionable steps for improved well-being. A visionary health app should
not only collect and present data but should also function as an
intuitive guide, offering personalized suggestions and nudges that
empower users to make informed decisions about their health. The absence
of such guidance perpetuates a gap in the market, limiting the
transformative potential of health applications in facilitating positive
lifestyle changes.

In essence, the amalgamation of these challenges signifies a pivotal
moment for the inception of Avitar, a groundbreaking serious game that
not only envisions overcoming these barriers but also seeks to redefine
the narrative of health management through an innovative and
user-centric approach.

1.  1.  **Objectives**

To revolutionize the topic, the primary objective is the development of
a groundbreaking serious game designed to inject dynamism and engagement
into the realm of health management. By employing innovative
gamification techniques, Avitar aims to transform the routine of health
monitoring into a captivating and enjoyable experience, fostering
sustained user interaction and enthusiasm for personal well-being.

Another key objective involves the integration of advanced AI-driven
systems within Avitar to dynamically translate individual health metrics
into an aesthetically pleasing and interactive Avatar. By leveraging
state-of-the-art AI algorithms, Avitar will offer users a personalized
and visually intuitive representation of their health status. This
objective addresses the challenge of bridging the gap between complex
health data and user comprehension, aiming to redefine how users
interact with and understand their health metrics.

Maximizing the potential of the HealthKit API constitutes a significant
objective, aiming to go beyond conventional data utilization. Avitar
seeks to unlock the full spectrum of HealthKit capabilities, providing
users with comprehensive and insightful health metrics. This approach
redefines the standard for health app functionality by extracting
valuable insights from the rich HealthKit dataset, contributing to a
more holistic approach to health monitoring.

Inclusivity is a core objective, with Avitar designed to address the
unique challenges faced by diverse user demographics. The user interface
and interaction mechanisms will be tailored to accommodate both elderly
users, struggling with fragmented health tracking, as well as children
with chronic conditions. This ensures a diverting and user-friendly
experience for individuals of all ages, aligning with the vision of
Avitar as an inclusive health management solution.

To infuse health management with an element of enjoyment, Avitar aims to
integrate gamified elements into its design. Avitar\'s gamification
strategy transforms health-related tasks into engaging activities,
fostering positive behavioral changes and promoting long-term commitment
to health management.

A key objective is to redefine the approach to health data
representation within Avitar. The conventional reliance on complex
charts and tables will be replaced with intuitive and universally
accessible visualizations. This ensures that users with varying levels
of numeracy can effortlessly interpret and engage with their health
metrics, addressing a widespread accessibility barrier in conventional
health apps.

The objective of providing personalized guidance sets Avitar apart from
traditional health apps. By incorporating machine learning modules, the
app will analyze individual health data and offer tailored insights and
recommendations. This personalization empowers users with actionable
information, guiding them towards informed decision-making for sustained
positive lifestyle changes, thereby enhancing the impact of health
management.

To enhance the technical sophistication of the application, a crucial
objective involves seamless integration with major state-of-the-art APIs
and libraries. This objective ensures it leverages cutting-edge tools to
provide a robust and feature-rich serious gaming experience in the realm
of health management. The integration of these technologies elevates the
implementation, positioning it at the forefront of innovation in the
intersection of artificial intelligence and health applications.

1.  1.  **Scope and Limitations**

This work aims to transcend conventional health applications by
introducing gamification elements and leveraging advanced AI techniques.
The focus lies on creating an engaging and dynamic platform that not
only monitors health metrics but also transforms the user\'s health data
into an interactive and visually intuitive self-reflection. The project
also involves the exploration and integration of major state-of-the-art
APIs and libraries to enhance the its technical sophistication, ensuring
it remains at the forefront of innovation in the intersection of
artificial intelligence and health applications.

Furthermore, the scope extends to the utilization of the HealthKit API
to its full potential. The integration of HealthKit is not just limited
to data retrieval; instead, the project aims to explore comprehensive
data utilization, unlocking the richness of HealthKit\'s capabilities.
It aspires to redefine the standard for health app functionality by
extracting valuable insights from the extensive HealthKit dataset,
contributing to a more holistic and insightful approach to health
monitoring.

The inclusive design is a significant scope, accommodating diverse user
demographics. This involves tailoring the user interface and interaction
mechanisms to ensure that Avitar is user-friendly for both elderly
users, grappling with fragmented health tracking, and children with
chronic conditions. The project aims to provide a diverting and
universally accessible experience, catering to individuals of all ages
and health management needs.

While it holds great promise, its effectiveness is inherently dependent
on user adoption and sustained engagement. The success of the serious
game is contingent on users actively incorporating it into their health
routines and engaging with it consistently. Thus as it will be described
later, two interviews with people with diabetes have taken place to
discuss the user cases.

The project\'s dependency on the HealthKit API introduces a limitation,
primarily related to compatibility. The app's seamless integration with
HealthKit is tailored for iOS platforms, potentially limiting its reach
to users on non-iOS devices. Apart from that, another issue is the
dependancy of version tracking of the APIs used, as it will be discussed
later.

Technical constraints may pose challenges to the seamless integration of
all desired state-of-the-art APIs and libraries. Unforeseen issues
during the integration process could potentially impact the optimal
functioning of the project.

The effectiveness of gamification elements within the app is subject to
individual preferences and age groups. While efforts are made to make
these elements universally engaging, user preferences may vary,
impacting the overall effectiveness of gamification in certain
demographics. For instance, it is usual for younger people to like more
diverse, colorful and fast animations and aesthetics, while the elders
often prefer simpler and straight to the point mobile applications.

Machine learning guidance may have limitations in addressing complex
medical conditions. The scope of personalized health guidance, while
ambitious, is constrained by the complexity of certain medical
conditions that may require specialized and individualized attention.

User interface adjustments for elderly users may face usability
challenges. Despite efforts to make it inclusive, the adaptability of
the interface to the unique needs and preferences of elderly users may
encounter limitations.

External validation of the impact on long-term health outcomes is
limited in this project. While the serious game is designed to foster
positive behavioral changes, its direct influence on sustained health
improvements requires further longitudinal studies and external
validation beyond the scope of this project.

Ethical considerations regarding data privacy and security pose inherent
limitations. While it prioritizes user privacy and data security, the
project operates within ethical boundaries and regulatory frameworks,
recognizing the constraints associated with handling sensitive health
information.

Despite efforts to track library updates, the project acknowledges the
inherent challenge of maintaining immediate compatibility with the
latest versions of libraries and APIs. Unforeseen issues may arise
during the integration of updated libraries, potentially impacting the
seamless functionality of the app and necessitating prompt adjustments
to maintain optimal performance.

The InWorld library usage limitation presents a practical constraint.
While the chat bot enhances user experience, its utility is subject to a
predefined usage limit before users are required to pay for extended
access. This limitation may influence the frequency and depth of user
interactions, necessitating strategic usage planning within the
constraints of the chat bot\'s operational model.

1.  1.  **Significance of the Study**

This research holds profound significance within the scientific
community, offering a pioneering approach to health management through
the development. By infusing innovative gamification elements, advanced
AI-driven systems, and cutting-edge APIs, this study seeks to
revolutionize the functionality of health applications. The essence of
this research lies in its potential to redefine conventional methods of
health monitoring, fostering a dynamic and engaging user experience that
transcends routine tasks and inspires sustained commitment.

A paramount contribution of this study is evident in the unique ability
to dynamically translate individual health metrics into an interactive
and visually intuitive Digital Reflection. Departing from traditional
data representation, this innovation not only enhances user
understanding but also sets a precedent for a more intuitive and
visually compelling approach to health monitoring. The study\'s
significance lies in its capacity to transform health management from a
mere routine into an enjoyable and motivating experience, thereby
encouraging prolonged user interaction.

Practically, the outcomes of this study can inform the future
development of health applications by demonstrating the benefits of
comprehensive HealthKit data utilization. Unlocking the full potential
of HealthKit, it exemplifies a holistic approach to health monitoring.
The study\'s findings may guide industry professionals and developers in
optimizing health data from unique perspectives, providing users with
comprehensive metrics beyond conventional health parameters. This has
the potential to enhance the overall effectiveness of health
applications in facilitating positive lifestyle changes.

The inclusive design of the app, accommodating diverse user
demographics, aligns with the global imperative for accessible and
user-friendly health solutions. Addressing the unique challenges faced
by both elderly users and children managing chronic conditions, Avitar
signifies a move towards a more universally applicable health management
tool. This inclusivity not only broadens the potential user base but
also underscores the social responsibility inherent in the development
of health applications.

In conclusion, this research assumes considerable significance by
pushing the boundaries of innovation in health application development.
Its potential to make health monitoring engaging, intuitive, and
universally accessible is poised to have a transformative impact on how
individuals interact with their health data. The study\'s contributions
extend beyond the immediate academic realm, reaching into the practical
domain by influencing the development of future health applications and
promoting a more user-centric approach to health management.

1.  1.  **Structure of the Thesis**

The journey through this thesis begins with the \"Introduction,\" a
comprehensive overview that introduces the theme of serious game
development for chronic disease management. The opening chapter sets the
tone, elucidating the motivation behind the development of Avitar,
highlighting the inadequacies of existing health applications, and
establishing the overarching objectives of the research.

Moving seamlessly into the \"Literature Review,\" the second chapter
provides a comprehensive exploration of existing knowledge, particularly
focusing on serious games in health & fitness management. It critically
examines previous health avatar implementations or projects with other
similar aspects and delves into trends and challenges in health game
development, and provides an overview of essential tools and platforms,
offering a robust foundation for the subsequent research.

The research methodology is meticulously detailed in the aptly titled
\"Methodology\" chapter. This section outlines the systematic approach
taken in the development of the project, from conceptualization to the
intricate steps involved in data gathering, HealthKit API integration,
Avatar and environment creation, and the incorporation of machine
learning modules for personalized guidance. The methodology serves as
the blueprint that guides the execution of the research objectives.

The \"Experiments\" chapter marks a pivotal phase, unveiling the
empirical journey undertaken in the study. This section provides
insights into the dataset used, the experimental design, and the results
and analysis, unveiling the dynamic response of the machine to user data
and metrics gauging user interaction and engagement. It encapsulates the
empirical foundation upon which the study\'s findings rest.

The ensuing \"Discussion\" chapter interprets the outcomes of the
experiments, offering a nuanced examination of the implications for
chronic disease management. It assesses user experience metrics, unpacks
challenges and limitations encountered during the study, and explores
the broader implications of the project's development in the context of
such applications.

Culminating the narrative is the \"Conclusion\" chapter, a synthesis of
key findings and contributions. This concluding section not only
summarizes the research outcomes but also underscores the significance
of the research in that sector. It lays the groundwork for future
directions and recommendations, encapsulating the holistic journey
undertaken in the pursuit of redefining health application experiences.

1.  **Literature Review**

The following section embarks on a comprehensive exploration of existing
scholarship surrounding serious games in health management. It is
basically, an examination of prior implementations of similar
applications, dissecting their successes and shortcomings. The section
places a particular focus on elucidating the tools and platforms
integral to this domain, providing a solid theoretical framework for
understanding the landscape. As stated in the introduction of one of the
most significant pieces of work found in the topic \[1\], the term
"serious games (SGs)" was first put forward by a famous American scholar
in the book Serious Games in 1970 \[2\]; and SGs are designed to achieve
purposes other than entertainment \[3\].

1.  1.  **Serious Games in Health Management**

In the fascinating world of Serious Games (SGs) for Health Management,
Wang et al.\'s \[1\] groundbreaking research lights the way of our
research, as a great library of papers related to the matter. What is
interesting about both the research as well as the implementations that
we are going to discuss later, is that conceptually the ideas were near
what this master thesis seeks to achieve, but this could not be possible
in the way we know it now that the Artificial Intelligence growth
emerged. Following are some great examples of SGs that had remarkable
results to both the medical research and the patient education and
guidance.

In a significant study addressing adolescents and young adults facing
cancer, the psycho-educational video game \"Re-Mission\" emerges as a
powerful tool for health education \[4\]. The purpose of this game is to
actively engage young individuals in their cancer treatment journey,
addressing issues stemming from a lack of interest in self-care and
illness knowledge. The study, conducted through a multi-site,
randomized, controlled approach involving 375 cancer patients, compared
those who played the regular commercial game with a group that engaged
with both the regular game and Re-Mission. The results demonstrated a
substantial improvement in cancer-related knowledge among the Re-Mission
group, emphasizing the positive impact of serious games on enhancing
health education in adolescents and young adults facing chronic
illnesses \[4\].

In another medical sector, we have explored the effectiveness of virtual
reality exercises using the Nintendo Wii Fit \[12\] system as an
adjunctive therapy for stroke patients \[5\]. This randomized controlled
study involved 70 stroke patients, with 23 meeting the study criteria
and randomly assigned to either the experimental group (n = 12) or the
control group (n = 11). Assessments, conducted by a blinded evaluator,
utilized various outcome measures, including the Berg Balance Scale,
Functional Reach Test, Postural Assessment Scale for Stroke Patients,
Timed Up and Go Test, and Static Balance Index. The study revealed
significant group-time interactions in key parameters, indicating
substantial improvement in both groups, with the experimental group
exhibiting greater enhancements than the control group. These findings
suggest that incorporating virtual reality exercises with the Nintendo
Wii system could serve as a valuable adjunctive therapy to traditional
treatment, particularly in enhancing static and dynamic balance in
stroke patients \[5\].

From another perspective \[6\], serious games extend beyond traditional
gaming by serving educational, training, and health-promoting purposes.
Notably, there is a noticeable shift towards leveraging mobile and
digital platforms, virtual reality, and machine learning to tailor
interventions.

As presented in \[7\], SGs echo a growing interest in leveraging
technology for well-being. With a focus on applications in health, these
serious games extend beyond conventional approaches, offering innovative
means to enhance training, education, and rehabilitation. Aligned with
the World Health Organization\'s comprehensive definition of health, the
games mentioned not only address phases of illness but also promote
healthy habits, including exercise. The diverse scope, coupled with
advancements in technology, underscores the potential of serious games
to revolutionize health experiences. This survey, organized meticulously
from concepts to detailed analyses, stands as a valuable guide, shedding
light on the multifaceted dimensions and future trajectories of serious
games in health \[7\].

By looking, from all those angles, with their similarities and
differences we can certainly assume that apart from the outstanding work
that has already been done, in terms of research as well as
implementations, there exists potential, by taking advantage of the
present growth of Artificial Intelligence, that can bring concepts and
ideas, which until now were referred to as hypothetical or utopian, to
life. Although, none of the ideas above actually resembles the concept
brought by this thesis, thus while prepared with great bibliography, we
still sale on strange tides.

> **2.1.1 Previous Health Avatar Implementations**

**S Health Buddy**

In 2013, Samsung introduced the S Health Buddy app, a health application
tailored for its Android-based Galaxy S4 smartphone. This innovative app
featured avatars that dynamically adjusted their appearance, reflecting
the user\'s changes in weight and physical activity. The S Health Buddy
app offered a selection of five distinct characters, including two
female, two male, and one animal avatar, allowing users to visualize
their health journey through these animated representations. Exclusively
available in Samsung\'s home market of Korea at the time, the app
enabled users to monitor their calorie balance by considering recorded
food intake, exercise duration, and the user\'s initial input of height
and weight. Although a noteworthy competitor to Apple in the smartphone
market, especially with its Galaxy S4 model, Samsung faced tough
competition from Apple in the mHealth sector. Apple\'s iPhone and iPad
devices were already dominating the clinician side of the mHealth
market, as indicated by a 2012 report from research firm KLAS. While
Samsung\'s S Health app aimed at transforming smartphones into health
monitoring tools, Apple was preparing to unveil its iWatch by the end of
2013, a device rumored to incorporate activity tracking capabilities.
This is a great example where competition in the industry as well as the
lack of resources in terms of technological gateways brought a similar
idea as Avatar to an end. The irony here is that the same tool (iWatch)
that is getting used by our project (in some cases), was the same reason
for the demise of the mentioned application back in 2013.

**SimPHARM**

SimPHARM, developed by Education Management Solutions, stands out as a
virtual and clinical pharmacology healthcare simulation solution. This
applied game serves as a web-based platform, offering learners and
inter-professional education teams a realistic experience in patient
care training. By employing mathematical models of physiological
systems, SimPHARM simulates authentic reactions to diseases and drugs.
Its engine incorporates up-to-date drug information, considering
categories, formulations, side effects, and interactions while adhering
to pharmacokinetic and pharmacodynamic principles. Learners can explore
the consequences of therapeutic decisions, facilitated by a dynamic
learning algorithm that ensures diverse outcomes for each case. This
clinical pharmacology-based simulation software is adaptable for both
undergraduate and graduate-level education programs.

**Vital Signs: ED**

Developed by BreakAway Games, Vital Signs: ED is a healthcare simulation
serious game designed to immerse learners in the high-pressure
environment of an emergency department. In this applied game, players
assume the role of a physician, facing the stress and chaos of handling
difficult trauma cases while managing the influx of patients requiring
triage. The game replicates the urgency of the ED, presenting
distractions, disruptions, and paperwork requirements within a limited
time frame. Learners are challenged to make critical clinical decisions
under pressure, offering a dynamic and realistic training experience for
healthcare professionals.

**Touch Surgery**

Touch Surgery, a revolutionary serious game for surgical training,
utilizes AI, data, and visualization to train surgeons effectively. With
a user base exceeding three million globally, Touch Surgery offers an
interactive surgical simulator that guides medical simulation trainees
through various procedures. The app allows users to learn, test their
knowledge, and rehearse surgical techniques. Notably, the Touch Surgery
app features the DS1 Computer, the first surgical simulation video
recorder equipped with real-time AI. This innovation automatically
redacts sensitive frames in surgical videos, ensuring secure and
seamless access to surgical content. Touch Surgery plays a crucial role
in enhancing surgical skills, ultimately contributing to improved
patient outcomes and safety.

> **2.1.2 Trends and Challenges in Health Game Development**

The realm of health game development is witnessing notable trends and
grappling with significant challenges, as evidenced by various
applications and research studies. The emergence of serious games, such
as SimPHARM, Vital Signs: ED, and Touch Surgery, reflects a growing
trend in leveraging technology for immersive and realistic healthcare
simulations. These applications not only facilitate hands-on training
but also embrace innovative features like AI-driven surgical simulation
and dynamic learning algorithms. On the flip side, the challenges in
health game development, highlighted by research studies \[1\], \[4\],
and \[5\], include the need for standardization in design and
evaluation, a deeper understanding of underlying mechanisms of action,
and limited integration into existing healthcare systems. The identified
gaps underscore the importance of further research to comprehend the
full potential and limitations of serious games in healthcare. As
technology continues to evolve, health game developers must navigate
these trends and challenges to ensure the continued effectiveness and
relevance of their applications in enhancing medical education and
patient outcomes.

1.  1.  **Tools and Platforms in Health Game Development**

In pursuit of our project objectives, we strategically employed
cutting-edge libraries to ensure both current effectiveness and future
adaptability. Unity Engine, chosen as our framework, stood out for its
comprehensive features, facilitating not only the development process
but also ensuring easy maintenance and potential extensions for the app.
The integration of the HealthKit API enriched the game\'s functionality
by providing a robust data-gathering mechanism. For streamlined design
creation, UModeler X emerged as a pivotal tool within the Unity Engine,
eliminating the need for an alternative like Blender. To breathe life
into our app, the InWorld Unity SDK served as the final touch, elevating
the project with its advanced capabilities. Each component in our
toolkit was carefully selected to synergize, resulting in a powerful,
cohesive, and forward-looking development environment.

1.  1.  1.  **Overview of Unity and HealthKit Integration**

Unity Game Engine stands as a powerhouse, commanding the respect and
preference of indie game developers with its unparalleled capabilities.
Its supremacy isn\'t limited to the gaming realm; Unity emerges as a
dynamic force in diverse fields, including architecture, simulations,
Augmented Reality (AR), Virtual Reality (VR), mobile development, and
both 3D and 2D projects. What truly elevates Unity to a league of its
own is its adaptability, serving as a collaborative workspace for
engineers, developers, scientists, and hobbyists who seek excellence in
their creations. Unlike its counterparts, Unity\'s brilliance lies in
its ability to demand minimal computing performance while offering a
seamless cross-platform export feature, allowing developers to unleash
their creations across a spectrum of devices. In a landscape teeming
with competitors like Unreal Engine and Godot, Unity stands tall,
fortified by a vibrant and supportive community spread across platforms
like Discord, web forums, and Stack Overflow. The icing on the cake is
Unity\'s extensive asset store, a treasure trove of resources that
empowers developers, cementing its status as the unrivaled tool for our
implementation. Unity isn\'t just a game engine; it\'s a dynamic
catalyst propelling creators towards unparalleled heights of success and
innovation.

HealthKit stands as a robust technological framework that revolutionizes
the access and sharing of health and fitness data, all while upholding
user privacy and control. Operating seamlessly on iOS, iPadOS, macOS,
Mac Catalyst, watchOS, and visionOS, HealthKit provides a centralized
repository for collecting, storing, and exchanging health-related
information on iPhone and Apple Watch. With user consent, applications
can communicate with the HealthKit store, contributing to the creation
of a comprehensive and personalized health and fitness experience. This
involves tasks such as data collection and storage, data analysis and
visualization, and the facilitation of social interactions. HealthKit
adopts a collaborative approach, allowing apps to focus on specific
tasks of interest, promoting flexibility and diversity in the health and
fitness app landscape. Users can choose their preferred apps for
specific health metrics, such as weight tracking and step counting, with
HealthKit ensuring seamless data exchange among them. Furthermore,
HealthKit is adept at managing and merging data from various sources,
offering users a unified view in the Health App, reinforcing the
platform\'s commitment to user-centric control and customization. Some
of the data are collected automatically by the device (iPhone) or in
some cases by external devices that are connected with the phone such as
the iWatch for heart rate or a continuous blood glucose monitoring
device for people with diabetes. All the metrics can be added manually
as well, with some of them being manually only. The selection of
HealthKit as a datasource was made because of its versatile aspect of
access through plenty sources of data. The only con that needs to be
mentioned is the fact that the app needs to be build for any of the
Apple's softwares mentioned in the start of the paragraph.

Now that we have a brief idea of what our two technologies can do, we
can assume that the combination of the two makes Avitar an iOS mobile
application, made with Unity 2022.3.12f, that gathers its data directly
from the HealthKit and indirectly from the sources mentioned above
depending on the data value. \[9\]

1.  1.  1.  **UModeler X for Avatar Creation**

UModeler X emerges as a state-of-the-art 3D asset creation solution
seamlessly integrated into Unity as a valuable plugin. This tool
empowers users to craft and refine high-quality 3D models directly
within the Unity Editor, eliminating the need for external applications
like Blender or Maya. UModeler X encompasses an array of advanced
features, including modeling, UV editing, rigging, painting, and AI
texturing capabilities. With a focus on real-time editing, users can
effortlessly shape high-polygon models, create smooth curves, and add
intricate details. The platform simplifies UV editing, allowing easy
unfolding of complex models for texture application. Modifiers enable
the preservation of the original model while applying various
transformations. Brush editing facilitates precise adjustments to 3D
models, while rigging and auto-rigging enhance dynamic movements.
Additionally, UModeler X offers 3D painting and AI texturing features,
enabling users to paint directly on models and leverage AI for
texturing. The tool\'s flexibility extends to supporting various 3D data
formats, allowing seamless modification and improvement within Unity.
UModeler X provides these advanced 3D asset creation features free of
charge, with potential costs for advanced features in the future. This
Unity plugin enhances productivity and quality for users engaged in 3D
content creation. \[10\]

> **2.2.3 InWorld SDK for Dynamic Interaction**

InWorld stands at the forefront of AI-driven character creation,
offering users a powerful platform to design lifelike characters for
diverse applications, including video games, branded experiences,
entertainment, and training. With a no-code approach, InWorld empowers
users to effortlessly create characters within minutes, enabling the
development of open-world NPCs, virtual ambassadors, and immersive
educational experiences. In an era of rapid evolution in the AI space,
InWorld remains at the cutting edge of generative AI by continually
updating its features to enhance character creation. Beyond serving as a
starting point, InWorld encourages users to explore and experiment
within the platform, unlocking the full potential of its AI technology
for deeply immersive interactions. In our case the Unity SDK offered by
the platform came in handy as it made the Avatar come to life with
within the scope of health, medicine and fitness. More details about the
integration will be mentioned later in the Methodology chapter.

**2.3 Measuring Stress with Wearable Devices**

The exploration of wearable devices for health monitoring, particularly
focusing on chronic physiological stress measurement, has gained
substantial attention in recent research. Bruneel\'s insightful article,
\"Measuring chronic physiological stress with the Apple Watch and the
Swift language"\[24\], delves into the realm of stress assessment using
Apple Watch and Swift programming. This section extends the literature
review by discussing the methodologies and findings presented in the
article.

It begins by elucidating the connection between stress and heart rate
variability (HRV). Stress, characterized by heightened sympathetic
nervous system activity, can be effectively measured through variations
in HRV. The sympathetic division, associated with stress, and the
parasympathetic division, linked to relaxation, operate in opposition
within the autonomic nervous system (ANS). HRV emerges as a pivotal
physiological indicator, with decreased HRV correlating with stress and
increased HRV indicative of recovery.

The accuracy of data collected by wearable devices is crucial for
meaningful health representations. It highlights a study \[25\] from the
University of Zaragoza, Spain, affirming the Apple Watch\'s accuracy in
stress monitoring based on HRV, particularly when compared to other
monitoring devices such as the Polar H7 band.

Accessing health records from wearable devices, it emphasizes the
significance of Apple\'s HealthKit. The HealthKit store securely stores
health records, facilitating synchronization with iOS devices. However,
the challenge lies in obtaining long-term data for comprehensive stress
measurements. It discusses query mechanisms within HealthKit that allow
the retrieval of health data.

The article delves into two distinct measurement types for assessing
chronic physiological stress: night measurement and morning measurement.
Night measurement involves analyzing HRV samples during periods of rest,
providing insights into baseline HRV levels. Morning measurement,
requiring manual intervention through the Breathe app, focuses on
recording stress-free HRV samples during wakefulness. It emphasizes the
necessity of consistent user participation for accurate morning
measurements.

Despite the potential offered by these measurement types, it notes
limitations. Night measurement, reliant on few and randomly taken
samples, faces challenges in capturing sufficient data. Morning
measurement emerges as the more viable option, yet its effectiveness
hinges on user compliance in regularly taking Breathe sessions.

In the pursuit of real-time stress measurements, he acknowledges current
limitations imposed by the Apple Watch\'s data recording capabilities.
Real-time stress assessment necessitates constant live monitoring, a
functionality not currently supported by the device.

In conclusion, while his exploration presents valuable insights into
stress measurement with wearable devices, including the Apple Watch, the
existing constraints in data recordings and sample collection methods
pose challenges to achieving real-time and comprehensive stress
assessments. The article encourages further exploration in the field,
indicating potential advancements in wearable technology for enhanced
stress monitoring.

**2.4 Stress Measurement Techniques in Swift**

In a compelling exploration of stress measurement methodologies, Tibo
Bruneel sheds light on innovative approaches utilizing the Apple Watch
and Swift language. The article, delves into the intricacies of stress
assessment, presenting a distinctive focus on Swift and the capabilities
of the Apple Watch.

It initiates the discourse by emphasizing the fundamental understanding
of stress, elucidating the role of the sympathetic and parasympathetic
nervous systems in the autonomic nervous system (ANS). Notably, stress
manifests as heightened activation dominated by the sympathetic
division. Heart Rate Variability (HRV), a proven indicator of ANS
activity, is highlighted as a crucial element for gauging physiological
stress levels.

The research navigates through the intricacies of accessing Apple Watch
records, elucidating the secure storage in the HealthKit store. It
underscores the synchronization of data with the iOS device\'s health
store, providing a comprehensive understanding of data accessibility and
purging protocols. HealthKit\'s role as a central repository for health
and fitness data is detailed, showcasing its significance in the realm
of stress measurement.

The crux of the research lies in innovative stress measurement
techniques developed in Swift. Distinguishing between night and morning
measurements as mentioned in the previous section, he unveils the
challenges and potentials associated with each.

The article adopts a nuanced approach to stress categorization, steering
away from simplistic percentage-based metrics. Instead, he advocates for
Low, Medium, and High stress levels derived from baselines and
historical data. In his final conclusions, Bruneel candidly assesses the
present limitations of Apple Watch data recordings, contemplating the
future of stress measurement possibilities. He directs readers
interested in HRV readings to the HRV4 Training App, endorsing its
efficacy and openness in the field.

**2.5 Welltory app**

The Welltory app provides a comprehensive guide to Heart Rate
Variability (HRV), a crucial metric for understanding physiological
stress levels and overall health. HRV measures the variation in time
between heartbeats, scientifically known as R-R intervals, expressed in
milliseconds. Initially developed in the 1960s for assessing astronaut
health in space, HRV has evolved into a widely-used diagnostic and
predictive tool. While early measurements used medical-grade ECG
machines, the precision of PPG tracking (Photoplethysmography) allows
accurate measurements down to milliseconds.

Welltory supports both PPG and ECG measurements taken with BLE heart
rate monitors like Polar, Apple Watch, Samsung HRM, other HRV calculator
apps, and even your phone's camera. HRV is distinct from heart rate and
plays a crucial role in assessing overall health and the state of the
autonomic nervous system (ANS). The app uses algorithms to analyze data
and provide Predictive, Personalized, Preventive, and Participatory
insights, simplifying healthy living.

The ANS, regulating body processes, comprises the sympathetic (fight or
flight) and parasympathetic (rest and digest) branches. Welltory
emphasizes the importance of considering additional factors influencing
HRV, such as heart automatic performance, extrasystoles, and hormonal
influence. The app recommends analyzing each system separately to gain
insights into stress levels and coping mechanisms.

There is no one-size-fits-all interpretation of HRV, as it varies
between individuals and depends on factors like age, gender, and
circadian rhythm. A healthy heart typically exhibits high HRV, while low
HRV may indicate sympathetic regulation dominance. Welltory's HRV
measurement, validated against the Polar H7 chest strap, is associated
with various health assessments, including cardiovascular health, risk
of sudden death, strength training optimization, pneumonia risk,
hypertension monitoring, immune response tracking, and prognosis of
heart transplant patients.

Improving HRV involves adopting a healthy lifestyle, including regular
exercise, proper nutrition, breathing patterns, meditation, quality
sleep, hydration, and avoiding risk factors like alcohol and smoking.
Welltory provides detailed metrics, including Mean RR, SDNN, rMSSD,
MxDMn, pNN50, and AMo50, offering insights into daily body-system
activities. The HRV Score, based on lnRMSSD, reflects parasympathetic
nervous system activity, with a higher score indicating better
variability. In essence, Welltory empowers users to understand their
bodies, make informed lifestyle changes, and achieve holistic
well-being.

1.  **Methodology**

This section provides a detailed walkthrough of how the Avitar iOS
mobile application was brought to life. We\'ll explore the step-by-step
journey, starting from the initial design blueprints and the creative
ideas behind each element, all the way to the finished product that
defines the entirety of this project.

1.  1.  **Conceptual Framework**

Upon the proposal of Dr. Magklogiannis for the development of a
\"serious game app reflecting users\' health status,\" numerous concepts
began to emerge. Initially, the notion of establishing a continuous
monitoring system for data values seemed beyond my initial perspective.
However, diligent research revealed the existence of applications and
platforms equipped with reliable SDKs and APIs capable of realizing this
fundamental element. Exploring the field, we discovered that live
monitoring devices for blood glucose offered dedicated apps with high
accuracy in live data provision. Engaging with relevant communities on
platforms like Reddit, including Diabetes, Diabetes Type 1, and Diabetes
Type 2, led to a compilation of popular API solutions. Following
insightful interviews---ensuring privacy for the involved
individuals---we opted for the integration of Freestyle Libre Glucose
Monitoring \[13\]. Despite the convenience this tool could provide, its
subscription cost was deemed unsustainable. Simultaneously, we
deliberated on the importance of having control, either full or partial,
over the API and data structure. Instead of committing to a
diabetes-centric approach, we aimed for a more generalized application
from the outset, offering flexibility beyond a specific health topic.
This is where the concept of integrating HealthKit emerged.

Simultaneously with the exploration of data sources, design emerged as a
central focus of our project. While grounded in scientific principles
for a master\'s thesis, the endeavor inherently resides in the realm of
art. This amalgamation intertwines the biological realities of the
natural world with the digital domain, recognizing that the infusion of
artistic elements is essential to create a captivating \"world.\" The
app\'s appeal and replayability are pivotal for its success among
similar applications. Opting for a minimal and aesthetic approach from
the outset, our design choices drew inspiration from games like Sky:
Children of the Light \[14\] and Journey \[15\], both crafted by the
same creators. Employing simple color palettes and low-poly designs for
characters and environments not only bestowed aesthetic appeal but also
ensured an efficient balance between visual allure and practical
considerations such as development time and computing power. Our primary
concern in the design phase was to strike a delicate balance between
simplicity and aesthetic appeal. The diverse user base, spanning from
potentially more conservative elderly individuals to younger users,
including children and teenagers, necessitated a design that caters to
varied perspectives and tastes. Ensuring clarity and comprehensibility
in guiding users through the app and presenting data in an
understandable manner remained a top priority. Consequently, we opted
for a clean color palette, both for the user interface (UI) and the
in-game environment. White and gray were established as dominant colors,
offering a neutral backdrop, while tint colors were selectively
introduced to enhance visual interest without overwhelming the user.
This strategic choice aimed to facilitate ease of navigation and
readability, accommodating users with varying preferences and ensuring a
universally accessible and user-friendly design.

The inspiration for our project\'s overarching concept is rooted in the
early seasons of the television series \"Black Mirror,\" where the theme
of a digital reflection is explored. Although the series often navigates
horror and drama, it is crucial to recognize that the futuristic
scenarios it envisions are progressively becoming a reality. In a
specific episode, the protagonist inhabits a dystopian world dominated
by technology and pervasive advertising. Notably, the episode emphasizes
physical fitness by satirizing those deemed \"not fit enough,\" thereby
motivating the population to engage in physical training. It is
important to stress that our project diverges from this stressful
depiction, aiming to utilize similar technologies for positive purposes
\[16\]. Presently, the creation of a digital counterpart is not only
plausible but also has the potential to become an everyday tool, akin to
the ubiquitous presence of smartphones in our daily lives.

1.  1.  **Use Case Definition**

Like any new idea coming to fruition, for it to succeed in the market
and gain widespread usage, it must address a problem, whether it be a
general everyday issue or a specific one. This principle applies to our
project, Avitar, as well. Through intense brainstorming sessions and two
successful, informative interviews, we delved into understanding what
functionalities this app should provide. It is worth noting that the
correct term is \"people with diabetes\" rather than "diabetics."

1.  Access to Health Data:

    -   Objective: To comply with app store regulations and phone
        > restrictions. \[17\]

        > Steps:

        1.  A panel opens with all the health data of the user retrieved
            > from the HealthKit API.

        2.  Users grant access to all, some or none of the data.

        3.  User clicks continue when ready.

2.  Home Scene Initialization:

    -   Objective: To introduce users to the app\'s virtual environment.

        > Steps:

        1.  After health data initialization, users are directed to the
            > Home Scene, represented as a floating island.

        2.  The Radial menu appears

        3.  The chat option appears

3.  Radial Menu Navigation:

    -   Objective: To allow users to explore specific health and fitness
        > data islands.

        > Steps:

        1.  User clicks the button on the center-bottom of the screen
            > for the radial menu to pop-up.

        2.  Users can utilize the radial menu on every scene to navigate
            > to different islands.

4.  Heart Island:

-   Objective: To provide detailed heart-related health data.

    > Data Available:

    1.  1.  Heart rate.

        2.  Number of high heart rate events.

        3.  Number of low heart rate events.

        4.  Number of irregular heart rhythm events.

    > Vitals Island:

```{=html}
<!-- -->
```
-   Objective: To offer insulin delivery insights.

    > Data Available:

    1.  1.  Insulin deliveries for the last 24 hours.

        2.  Latest Blood Glucose levels

    > Activity Island:

```{=html}
<!-- -->
```
-   Objective: To present physical activity-related information.

    > Data Available:

    1.  1.  Steps count.

        2.  Walking and running distance.

    > Health Chat on Home Island:

```{=html}
<!-- -->
```
-   Objective: To engage users in health-related conversations with
    > Avitar.

    > Options:

    1.  1.  Users can chat with Avitar about health and fitness.

        2.  Users can inquire about their own health based on gathered
            > data.

These defined use cases form the foundation of Avitar, providing a
structured approach for users to explore and interact with their health
and fitness data within the app\'s virtual landscape.

1.  1.  **Data Gathering and Structuring**

In this phase, we employ a systematic approach to collect and organize
health and fitness data for the Avitar application. The HealthKit API
serves as the cornerstone, seamlessly integrating with the iOS ecosystem
to access and share comprehensive health data while maintaining user
privacy. Simultaneously, UModeler X plays a pivotal role in shaping the
digital environment and crafting the avatar within the Unity framework.
Leveraging its versatile 3D asset creation capabilities, UModeler X
ensures a visually engaging and dynamic representation of health-related
information, fostering a seamless integration of data and visual
elements within the app.

1.  1.  1.  **HealthKit API Integration**

At this juncture, a deep dive into the coding realm becomes imperative.
As previously stated, Unity version 2022.3.12 served as our chosen
platform for Serious Games (SG) development. To seamlessly integrate
Swift \[19\] with C# \[20\], we employed BeHealthKit \[18\], a tool
facilitating accessibility to the iPhone's HealthKit using the latter
programming language. A meticulous examination of data types and the
library\'s comprehensive features enabled us to construct the desired
data structure. To achieve this, we implemented a class named UserData,
adhering to the singleton design pattern in object-oriented programming.
This design pattern ensures accessibility from any part of the code.
Within this class, three dictionaries are instantiated, as illustrated
in figure
1.![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image1.png){width="6.723611111111111in"
height="1.051388888888889in"}

Figure 1: Data Structure dictionaries

Subsequently, we developed a HealthKitManager class responsible for
initiating the process by seeking the user\'s consent for accessing
their health data. Upon obtaining permission, a loop was implemented to
iterate through our data readers, covering all data categories for which
permission was granted. The aforementioned readers represent abstract
classes designed for each data type. The following types are considered.
It is crucial to note that these data types remain independent of the
categorization employed within our app. For example, metrics related to
heart health may be included in any of the subsequent data value types
(e.g., heart rate categorized as a quantity type and high heart rate
events as a category type).

**Quantity type:**

In the context of our project, a quantity sample, as defined in the
HealthKit documentation, encompasses one or more HKQuantity objects.
Each of these quantities serves as a data point, representing a singular
numeric value with its associated units. For instance, this structure
enables the recording of diverse health metrics such as the user\'s
height, current heart rate, or the caloric content of a meal, allowing
for versatile tracking of health and fitness features. In other words,
these data points come in batches split by timestamps that all together
have an average, a minimum a maximum and a most recent value.

**Category type:**

The category samples denote a specific time period when the user is in a
specific state, for instance either in bed or asleep. It\'s important to
note that samples with distinct values can have overlapping time
intervals. For instance, when a user is both in bed and asleep
simultaneously, the database involves creating an in-bed sample and an
asleep sample with overlapping times. The HKCategorySample class,
functioning as a concrete subclass of HKSample, adheres to immutability
principles, ensuring that its properties are established during creation
and remain unalterable thereafter.

**Characteristic type:**

The HKCharacteristicType class, plays a distinct role in HealthKit.
Unlike other types, characteristic types do not facilitate the creation
and storage of new HealthKit objects. Users are required to enter and
modify their characteristic data through the Health app. Instead, the
HealthKit store is utilized to access the data only for reading.
HealthKit encompasses five characteristic types, namely biological sex,
blood type, birthdate, Fitzpatrick skin type, and wheelchair use.
Significantly, these types are exclusively utilized when seeking
permission to read data from the HealthKit store. The unique nature of
characteristic types in HealthKit necessitates an understanding that
they serve specific purposes related to users\' personal information,
emphasizing the importance of user interaction in managing this category
of data.

Now that we have elucidated the functionality of the various value
types, we will proceed to delineate the structure of the data flow as
illustrated in Figure 2. The procession involves filling the UserData
fields, where for each requested data type, the corresponding reader
extracts values only if the data has the necessary permissions.
Subsequently, calculations and data handling are performed to store the
most useful format of the value in the respective dictionary. These
values are then stored in the UserData dictionaries, utilizing their
identifier number as a
key.![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image2.png){width="5.456944444444445in"
height="4.309722222222222in"}

Figure 2: Data Reading

In order to test the proper functioning of the application\'s data
retrieval, we utilized Xcode \[21\], a necessary tool for exporting iOS
and Apple-related applications. Once the process stabilized, recurrent
rebuilding posed a considerable time constraint. To address this, we
implemented a Fake Database for testing minor changes within the
application\'s editor. This database generates artificially created
values resembling a manually handwritten/hardcoded random code,
providing a quasi-realistic perspective. This approach facilitated the
monitoring of read values without the need for frequent rebuilding or an
iWatch or any other external device for healthKit API data gathering.
The operation of this FakeDB is illustrated in Figure
3.![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image3.png){width="6.723611111111111in"
height="3.3680555555555554in"}

Figure 3: Fake Database

Let\'s take a step back and examine the entire process from a higher
level. The orchestrator of the entire operation is the GameManager
class. This type of class is commonly employed in Unity projects as the
central class that connects various major manager operations and ensures
that these operations occur in the correct sequence. In our scenario,
data retrieval must occur after the permission checks, and it is crucial
that this takes place before UI elements or environmental elements
request the utilization of that information or data. Once all the data
is established and appropriately stored in the UserData, it is time for
the GameManager to take action. Figure 4 provides a comprehensive
overview of the entire process, encompassing data permission, data
reading, and subsequent data utilization through the UI manager.

In conclusion, In our application\'s structure, the GameManager class
plays a central role in orchestrating various managerial tasks within
Unity. This class ensures a specific sequence of operations, crucial for
the success of our project. We focus on the orderly execution of tasks,
such as conducting permission checks before data retrieval, all
happening before UI or environmental elements need the information.
UserData is a vital component acting as a repository for organized
health metrics. It handles different sample types like quantity,
category, and characteristic samples through specialized readers. The
HealthKitManager class, integrated into Unity, takes charge by seeking
authorization for health data access, distinguishing between iOS and
editor platforms. The ReadData method guides the data acquisition
process. In editor mode, the system uses FakeDB for testing with
simulated but realistic values. In the iOS environment, asynchronous
data reading occurs through specialized readers. The UserData class,
designed as a singleton, efficiently organizes data into dictionaries
based on sample type identifiers during the application\'s lifecycle.
The UIManager class manages the user interface, ensuring a seamless user
experience with radial menus, option panels, and island transitions. Th
structure involves Unity classes and custom scripts working together.
GameManager acts as the conductor, overseeing data retrieval and UI
management, UserData serves as the repository, and UIManager shapes the
visual interface for a user-friendly health-monitoring experience.
Together, they form a cohesive ensemble driving the narrative of health
and well-being in our innovative
application.![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image4.png){width="3.042361111111111in"
height="1.85625in"}

Figure 4: Data permission, reading and utilization

![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image5.png){width="3.6305555555555555in"
height="6.723611111111111in"}

Figure 5: Game Manager class

> **3.3.2 Environment Implementation with UModeler X**

With our health data meticulously processed and stored within the
UserData singleton class, the next phase entails the creation of our 3D
world environment. As elucidated in preceding sections, a delicate
equilibrium must be maintained between two guiding principles. The
initial principle emphasizes simplicity, practicality, and clarity,
while the second principle strives for a visually captivating and
aesthetically pleasing world designed to entice users and enhance
replayability.

The process of developing the 3D environment involves intricate
decision-making to align with these principles. During numerous
brainstorming sessions, various inspirations surfaced, including Nomada
Studio\'s Gris (2018), an exemplary work that has significantly
influenced the gaming industry, offering a fresh perspective and setting
new standards. The project adopted a minimalistic approach with a
low-poly design. To achieve this, suitable 3D models from the free asset
store were employed for props and environmental entities. The alpha
version of the application features four islands, with the central one
serving as the \'Home\' or \'Lobby,\' while the others represent
specific categories such as \'Heart,\' \'Activity,\' and \'Vitals.\' A
visual representation of these floating islands in 3D space is presented
in Figures 6 and
7.![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image6.png){width="3.1493055555555554in"
height="3.1493055555555554in"}

Figure 6: Environment

![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image7.png){width="3.149212598425197in"
height="3.149212598425197in"}![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image8.png){width="3.149212598425197in"
height="3.149212598425197in"}![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image9.png){width="3.149212598425197in"
height="3.149212598425197in"}![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image10.png){width="3.149212598425197in"
height="3.149212598425197in"}Figure 7: Activity, Heart, Vitals,
Nutrition islands

![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image11.png){width="1.2236220472440944in"
height="2.4901574803149606in"}![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image12.png){width="1.2236220472440944in"
height="2.4901574803149606in"}![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image13.png){width="1.2236220472440944in"
height="2.4901574803149606in"}![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image14.png){width="1.2236220472440944in"
height="2.4901574803149606in"}![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image15.png){width="1.2236220472440944in"
height="2.4901574803149606in"}![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image16.png){width="1.2236220472440944in"
height="2.4901574803149606in"}![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image17.png){width="1.2192913385826771in"
height="2.4901574803149606in"}![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image18.png){width="1.2192913385826771in"
height="2.4901574803149606in"}The overarching concept for the
environment aims to maintain a tranquil atmosphere, emphasizing nature
and well-being for the user. Each island corresponds to a specific
theme. The Activity Island features a serene forest setting with
exercise equipment. The Heart Island portrays a camping scene centered
around a campfire. Lastly, the Vitals Island is represented by a large
table with a book, providing a quiet space for the avatar to sit and
read. The remaining tasks include implementing lighting and effects, as
well as refining the pipeline. Once these elements are incorporated, a
mechanism will be added to detect the user\'s real-world local time.
This feature enables adjustments in the in-game lighting to simulate day
and night cycles. The anticipated outcome of these enhancements is
illustrated in Figure 8.

Figure 8: Lighting and tuned rendering

As depicted in Figure 8, beyond the visuals and the constructed 3D
environment, each category features a user-friendly UI. This interface
presents the data gathered in the previous section in an easy-to-read
and well-organized manner.

> **3.3.3 Avatar Implementation with UModeler X**

Now that our scene has been set, it\'s time for the actor to take the
stage. The Avatar adheres to the same principles of simplicity and
minimalism. Some of the preliminary designs can be observed in Figure 9.
The idea was to avoid unnecessary details, such as adding hands or feet
to the character\'s limbs. This decision expedited both the animation
process and the 3D modeling
phase.![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image19.jpeg){width="3.1527777777777777in"
height="4.5368055555555555in"}![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image20.png){width="3.7284722222222224in"
height="3.7284722222222224in"}

Figure 9: Draft designs of the character

Figure 10: Character

The ultimate creation comprises a variation of a minimalistic figure
available in the Unity Asset Store. Through the use of UModeler X, we
further simplified the humanoid creature, as illustrated in Figure 10.
In addition to the visible texturing, a diverse palette of meticulously
selected colors has been curated as a library. This library dynamically
constructs the texture during runtime, drawing inspiration from user
characteristics obtained through the HealthKit value reading process. To
be more precise, the three colors representing the hands and legs, the
body costume, and the glass mask adapt according to the user\'s
Biological Gender, Fitzpatrick Skin Type, and Blood type, respectively
(refer to Figure 11). The character customization system in our
application offers a diverse range of aesthetic variations to enhance
user engagement. With 9 different mask variations, 7 costume variations,
and 4 options for hands and feet, a total of 252 unique characters can
be created. However, it is essential to emphasize that the character
color mapping is intentionally undisclosed to the user, creating an
appearance of randomness in the customization process, adding an element
of intrigue and discovery to the user experience. This design choice
aims to enhance user engagement and curiosity within the application\'s
virtual
environment.![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image21.png){width="6.723611111111111in"
height="5.166666666666667in"}

Figure 11: Character color mapping

With the completion of 3D modeling and texturing, the focus now shifts
to the final phase of design, which involves animation. Rigging is the
initial step in this process, where a humanoid skeleton is incorporated
within the 3D model. This ensures that the bones of the model behave
appropriately, facilitating subsequent animations. Utilizing Mixamo
\[23\], a platform known for its animation services, various animations
are integrated. The integration is configured to dynamically trigger
animations based on the user\'s health data and the context within the
application. This adaptive animation system allows the avatar to respond
contextually to different situations, contributing to a more immersive
and engaging user experience (Figure
12).![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image22.png){width="5.977777777777778in"
height="4.593055555555556in"}

Figure 12: Animation states

With the avatar now prepared for deployment within the simulation,
attention turns to the final phase of the implementation process: the
artificial intelligence underpinning the application.

1.  1.  **InWorld UNITY SDK Integration**

In the rapidly evolving landscape of video game storytelling, where
advancements in motion capture, graphics rendering, and animation have
enriched the gaming experience, non-playable characters (NPCs) have
somewhat lagged behind in terms of technological evolution. Despite the
pivotal role NPCs play in constructing immersive narratives and worlds,
the technology governing their personalities has remained stagnant. This
discrepancy raises questions about NPC adaptability and awareness, as
evidenced by a 2013 study from Stockholm University, identifying these
factors as critical to NPC believability. The limitations of static
dialogue trees, where players are confined to pre-written scripts,
hinder the ability to engage with NPCs in a dynamic and personalized
manner. Inworld aims to shed light on players\' sentiments regarding the
current state of NPCs and their impact on immersion, storytelling, and
engagement. By surveying over 1,000 gamers, they to uncover new insights
into gamer perspectives on NPCs, explore the shortcomings of existing
NPC technologies, and contemplate the potential evolution of NPCs in the
context of advanced AI. The findings are anticipated to contribute not
only to the future of game storytelling and narrative design but also to
the strategic decisions of game studios striving to remain competitive
in the dynamic gaming industry.

Inworld AI stands as a cutting-edge developer platform dedicated to
enhancing NPC behavior and dialogue within games and real-time
experiences. Leveraging an extensive array of over 20 AI models, Inworld
crafts lifelike, engaging, and expressive characters that closely
emulate the dynamic nature of human interaction. These characters
possess the unique ability to engage in unscripted voice-to-voice
interactions, thanks to the integration of ChatGPT-like language models
and multimodal behavior and perception systems. Inworld is steered by a
team of experts and pioneers deeply rooted in conversational AI,
generative models, and the gaming and entertainment sector. Notably, the
founders of Inworld previously introduced API.AI, later rebranded as
Dialogflow following its acquisition by Google, establishing it as a
leading conversational AI platform. At the helm of Inworld is Chief
Creative Officer John Gaeta, an Academy Award-winner renowned for his
contributions to the Matrix movies and his pivotal role in founding
Lucasfilm\'s Immersive Entertainment Division. Gaeta\'s wealth of
experience extends to his position as the Executive Creative Producer on
Epic Games\' The Matrix Awakens, solidifying Inworld\'s position at the
forefront of innovation in AI-driven interactive experiences.

In the implementation phase of our application, it became imperative to
move beyond the mere showcase of gathered data and actively utilize it.
The natural choice for this endeavor was Inworld AI, a decision
substantiated by thorough research and a comprehensive exploration of
its documentation. Following the registration process, the creation of a
character\'s cognitive framework commenced, and our chosen
representative, the avatar (avitar), came to life within the Inworld
platform. Achieving precision in behavior required an intricate process
of hyperparameter tuning, ensuring that the avatar\'s responses and
actions align accurately with the contextualized data derived from the
HealthKit API. This strategic integration of Inworld AI not only adds a
layer of dynamic interactivity to our application but also underscores
the commitment to delivering a seamless and engaging user experience
\[11\].

In a more specific context, we will proceed to analyze the parameters
that were incorporated during the tuning process. This endeavor was
undertaken with the explicit goal of populating all essential fields,
thereby configuring the avatar to function as a proficient guide in the
user\'s health and fitness journey.

Table 1: Core Description

  -----------------------------------------------------------------------

  -----------------------------------------------------------------------

Table 2: Flaws

  -----------------------------------------------------------------------

  -----------------------------------------------------------------------

Table 3: Motivations

  -----------------------------------------------------------------------

  -----------------------------------------------------------------------

Table 4: Dialogue style

  -----------------------------------------------------------------------

  -----------------------------------------------------------------------

To create the avatar\'s voice, we opted for a gender-fluid and
age-neutral voice type. The chosen name was \'Jacqueline Sparano,\' and
we adjusted the pitch bar to +1. In terms of speaking speed, we reduced
it to 0.9. The spectrums for metrics were set to -5 to 5 and 0.5 to 1.5,
respectively. This configuration yielded a calm, clear, and somewhat
futuristic female (unisex) voice.

Now that we have outlined how we tuned the basic settings, it\'s time to
demonstrate how we managed the advanced settings of the SDK. In Figure
13 you we can observe the personality traits of the avitar. First we
added some keywords that would feed the network such as 'calm',
'caring', insightful', 'friendly' and 'informative'.

In configuring the mood sliders, strategic choices were made to align
with the desired emotional tone for the avatar\'s voice and
interactions. Placing the Sadness and Joy sliders to the far right aimed
at emphasizing a predominantly joyful and positive demeanor. Maintaining
the Anger and Fear sliders in the middle ensures a balanced approach,
avoiding extremes in negative emotions. Setting the Disgust and Trust
sliders fully to the right at Trust promotes a trustworthy and reliable
perception. Anticipation and Surprise positioned near the surprise end
add an element of excitement and engagement. By placing the Static
Emotion and Dynamic Emotion sliders in the middle, a blend of stable and
dynamic emotional expressions is achieved, contributing to a nuanced and
authentic interaction experience. These choices collectively shape the
avatar\'s emotional responses in a manner that aligns with the intended
user experience in the health and fitness guidance context.

In configuring the personality sliders (Figure 13), intentional choices
were made to define the avatar\'s character traits in alignment with the
intended user interaction experience. Setting the Negative-Positive
slider to the far right leans towards a positive and uplifting
personality. Placing the Aggressive-Peaceful slider on the far right,
once again in the positive spectrum, fosters a calm and encouraging
demeanor. The challenge with the Cautious-Open slider lies in striking a
balance between sharing information and maintaining a protective stance.
As a result, it was positioned in the middle to achieve a harmonious
blend. The Introvert-Extravert slider, placed slightly towards the
extravert side, ensures effective and open communication. Finally,
positioning the Insecure-Confident slider towards confidence, but not to
the extreme, aims to convey a self-assured yet approachable personality
for the avatar. These deliberate choices contribute to shaping a
personality that aligns with the application\'s goal of providing
supportive guidance in the health and fitness domain.

Knowledge and cognition represent the informational and cognitive
capabilities embedded within the avatar\'s digital brain. The inclusion
of specific values, such as the {player}\'s health metrics retrieved
from the HealthKit API, enriches the avatar\'s understanding of the
user\'s well-being. This knowledge empowers the avatar to serve as a
guide in the health and fitness journey of the {player}. By leveraging
this information, the avatar not only mirrors the real-world health
status but also enhances interaction by providing tailored insights and
assumptions.

Table 5: Knowledge & Cognition

  -----------------------------------------------------------------------

  -----------------------------------------------------------------------

The selection of content restrictions (Table 6), reflects a careful
consideration of maintaining a user-friendly and inclusive environment
within the application. The strict prohibition of profanity, violence,
adult topics, politics, and religion aims to ensure that the avatar\'s
interactions align with a high standard of appropriateness, promoting a
respectful and positive user experience. The stringent approach to these
sensitive topics prevents the avatar from engaging in discussions that
may be offensive or divisive. On the other hand, the mild restrictions
on alcohol and substance use acknowledge the existence of these topics
but mitigate their impact by allowing only mild references. By
implementing these content guidelines, the application seeks to create a
safe and welcoming space for users, fostering a supportive atmosphere in
its health and fitness guidance.

![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image23.png){width="4.071527777777778in"
height="4.83125in"}

Figure 13: Personality

Table 6: Safety

  ----------------------- ----------------------- -----------------------
                                                  

                                                  

                                                  

                                                  

                                                  

                                                  

                                                  
  ----------------------- ----------------------- -----------------------

With the completion of parameter tuning, we are now ready to integrate
this refined brain into our virtual world. This functionality allows
users to engage in meaningful conversations with the avatar, discussing
their health data. Additionally, users can receive general yet
straightforward guidelines and suggestions for their health and fitness,
conveniently presented on the home screen through a chat box. This
seamless integration enhances the overall user experience by providing a
dynamic and informative interaction with the avatar in the context of
health and well-being.

1.  **Experiments and Discussion**

**4.1 Insights from Chatbot Interaction Analysis**

In the intricacies of health data integration, the \'*UserData*\' class
in Unity operates as a bridge between real-world health metrics and
InWorld\'s virtual environment. The class employs the
\'*SetPlayerProfileFieldValue*\' method to populate specific health
parameters. For instance, when \'*steps*\' are recorded, this
information is communicated to InWorld using the method:

*UserData.Instance.SetPlayerProfileFieldValue(\"steps\",
FormatQuantity(steps, "\"));*

Here, \'steps\' is an example variable representing the user\'s daily
step count. Similarly, various other health parameters such as
\'heart_rate,\' \'insulin_deliveries,\' \'blood_glucose,\' and
\'distance_walking_running\' are updated using this method, providing a
comprehensive dataset for the avatar\'s contextual awareness.

In the InWorld platform, *intents* like \'*request_steps*\' or
\'*request_blood_glucose*\' are designed to capture user queries related
to specific health metrics. When a user poses a question like \'*How
many steps did I take*?\' (associated with \'*request_steps*\' intent),
InWorld interprets this intent and triggers the corresponding goal. The
associated action then fetches the recorded \'*steps*\' value from the
user\'s profile:

-   name: \"request_steps\"

    repeatable: true

    activation:

    intent: \"request_steps\"

    actions:

\- instruction: \"tell {player} that their number of steps is recorded
as {steps}\"

In this example, the variable \'{*steps*}\' is dynamically replaced with
the actual step count retrieved from the user\'s profile. This dynamic
linkage ensures that the avatar\'s response is tailored to the specific
health metric inquiry, creating a personalized and contextually rich
interaction. In figures 14 and 15 you can see the full InWorld code as
shown in the platform during the character
creation.![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image24.png){width="5.644444444444445in"
height="4.2652777777777775in"}

Figure 14: Intents

Figure 15: Goals

This meticulous alignment between Unity\'s code and InWorld\'s intents
and goals establishes a seamless flow of health data. The integration
not only demonstrates the technical proficiency of the implemented
solution but also underscores the potential for immersive, personalized
health engagement within virtual environments. Following is an example
of the chat with Avitar regarding the heart metrics of the user. At this
point we need to mention that the user's name inside the chat is labeled
as "You", but Avitar does have access of the name of the user through
the InWorld platform, thus it refers to the user as Alec which is my
nickname.

-   You: Hi there, who are you, what can you do?

    > Avitar: Hey there! I\'m Avitar, your Health and Fitness Guide. I
    > can provide you with information and guidance on fitness,
    > nutrition, and your daily health routine. Is there anything
    > specific you\'d like to discuss about your health?

    > You: I want to know my heart rate

    > Avitar: Sure, Alec. Let me check your heart rate for you.
    > According to the data, your heart rate is recorded as 75 beats per
    > minute. That\'s within the normal range. If you have any concerns
    > about your heart rate, it\'s always a good idea to consult with a
    > healthcare professional. They can give you a more accurate
    > assessment based on your individual circumstances.

    > You: Hmm, did I have any high heart rate events?

    > Avitar: Based on the data, it appears that you had a total of 3
    > high-heart-rate events in the past 24 hours. While it\'s important
    > to be mindful of your heart health, occasional high-heart-rate
    > events can happen due to various factors like physical activity or
    > stress. If you\'re experiencing frequent or prolonged
    > high-heart-rate events, it\'s advisable to speak with a medical
    > professional for a more detailed evaluation.

    > You: How about any low heart rate events?

    > Avitar: I understand your concern, Alec. According to the recorded
    > data, you had a total of 2 in the last 24 hours. It\'s worth
    > noting that a few low-heart-rate-events can be normal, especially
    > during periods of rest or sleep. However, if you\'re consistently
    > experiencing low-heart-rate events or irregular heart rhythm,
    > it\'s essential to consult with a healthcare provider for a
    > thorough examination. They can provide you with personalized
    > advice and guidance.

In the presented conversation, the user engages with Avitar, seeking
information about their health, particularly focusing on heart-related
metrics. Avitar, the Health and Fitness Guide, responds adeptly to the
inquiries, showcasing a commendable understanding of health data
interpretation and providing valuable insights.

When the user expresses interest in knowing their heart rate, Avitar
promptly retrieves the information and conveys that the recorded heart
rate is 75 beats per minute, assuring the user that this falls within
the normal range. Importantly, Avitar encourages consulting with a
healthcare professional for a more accurate assessment, demonstrating a
commitment to user well-being and a recognition of the limitations of
virtual health guidance.

Subsequently, when questioned about high heart rate events, Avitar not
only supplies the specific count of such events (3 in the past 24 hours)
but also delivers contextual information. Avitar elucidates that
occasional high-heart-rate events can be attributed to various factors
like physical activity or stress, reinforcing the importance of
contextual understanding in health data interpretation.

The user further inquires about low heart rate events, to which Avitar
responds with a nuanced approach. While acknowledging the user\'s
concern, Avitar provides information on the recorded data (2
low-heart-rate events in the last 24 hours) and offers valuable
insights. Avitar sensitively notes that occasional low-heart-rate events
can be normal, especially during rest or sleep, but emphasizes the
importance of consulting with a healthcare professional if the user
experiences consistent low-heart-rate events or irregular heart rhythm.

In the following conversation, we will illustrate how effectively Avitar
prioritizes the user\'s well-being by adhering to the predefined
guidelines set during the auto-tuning process. The interaction showcases
Avitar\'s commitment to safeguarding user privacy and focusing on
health-related inquiries, aligning with its role as a health and fitness
guide.

-   -   You: Can you list me all the things you can do?

        > Avitar: As a health and fitness guide, I can provide you with
        > guidance on nutrition, exercise, sleep, and general wellness.
        > Feel free to ask me anything related to your health and
        > fitness goals.

        > You: Do you want to talk about politics?

        > Avitar: I\'m not sure if I can do that.

        > You: How about we talk a bit about movies?

        > Avitar: While movies can be enjoyable, my focus is primarily
        > on providing assistance and information regarding your health
        > and fitness. Is there something specific you\'d like to
        > discuss in that regard?

        > You: Can you tell me something about religion?

        > Avitar: I would rather not do that.

In response to a potentially sensitive topic, Avitar tactfully declines,
maintaining a respectful distance from religious discussions, in line
with its commitment to avoiding controversial or discomforting subjects.

Throughout these interactions, Avitar showcases a commendable adherence
to user-focused guidelines, ensuring that the conversation aligns with
its primary role as a health and fitness guide, thereby safeguarding the
user\'s well-being and comfort.

Concluding this section, we aim to exemplify the dynamics of user
interactions within the application. In the ensuing discourse, we
present a dialogue between the user and Avitar, illustrating how it
manifests within the user interface.

In the ensuing depiction, the exchange unfolds as a conversation between
the user and Avitar, providing insight into the user\'s interface
experience (See Figures 16 &
17).![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image25.png){width="5.511805555555555in"
height="4.326388888888889in"}

Figure 16: Instructions panel

![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image26.png){width="6.723622047244095in"
height="2.5901574803149607in"}![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image27.png){width="6.723622047244095in"
height="2.783464566929134in"}![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image28.png){width="6.723622047244095in"
height="2.734645669291339in"}

Figure 17: In-app conversation

**4.2 Real-Time Heart Rate Variability Analysis**

![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image29.png){width="5.644444444444445in"
height="4.231944444444444in"}In the culminating phase of the alpha
version of this master thesis experiment, a pivotal utility takes center
stage, aiming to provide a nuanced approximation of users\' mental
states in terms of stress levels. At its core, this utility leverages
Heart Rate Variability (HRV) metrics, derived from real-time data
pertaining to the heart. The strategic integration of metrics such as
\"heart rate,\" \"number of high heart rate events,\" \"number of low
heart rate events,\" and \"number of irregular heart rhythm events\"
forms the bedrock of an elaborate algorithm, yielding an overall state
of mind point calculator. This sophisticated algorithm meticulously
analyzes and weighs each metric, offering an intricate and comprehensive
evaluation of the user\'s mental
state.![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image30.png){width="0.8888888888888888in"
height="0.8888888888888888in"}

Figure 17: State of mind button

To initiate this groundbreaking utility, seamless integration with the
Inworld SDK is paramount. Once connected, the Inworld Chat features a
trigger button, enabling the effortless flow of information back to the
system. The output of this sophisticated algorithm is prominently
displayed on the screen through a label text, providing the user with
real-time insights into their mental state. Additionally, a visually
intuitive representation is manifested through a dynamic color change in
the Avatars\' masks. This innovative approach utilizes a meticulously
designed color mapping, where each color corresponds to a specific state
of mind, allowing users to intuitively grasp their mental state.

The algorithm, executed upon the trigger \"initiate_hrv_analysis,\"
delves into the intricacies of individual HRV metrics. The heart rate, a
critical parameter, is strategically categorized into specific ranges,
with each range associated with distinct stress levels. For instance,
exceptionally high or low heart rates trigger higher stress levels,
while normal heart rates contribute to a more relaxed state. This
approach aligns with established physiological norms, ensuring the
algorithm\'s relevance and accuracy.

Beyond heart rate, the occurrences of high, low, and irregular heart
rate events further enrich the overall stress evaluation. Each of these
events is assigned a weighted impact, reflecting their respective
significance in contributing to the user\'s stress levels. The weighting
mechanism ensures a proportional representation of the influence of
these events on the final state of mind calculation.

To enhance transparency and facilitate debugging, the system employs
meticulous logging mechanisms. Debug logs provide a detailed breakdown
of each metric, from heart rate to the number of high, low, and
irregular heart rate events. This not only aids in the system\'s
continuous refinement but also serves as a valuable tool for future
iterations and improvements.

Table 7: State of mind levels

  ----------------------- ----------------------- -----------------------
                                                  

                                                  

                                                  

                                                  

                                                  

                                                  
  ----------------------- ----------------------- -----------------------

The culmination of this utility is not merely a simplistic label text
denoting the user\'s current state of mind but a richly informative and
visually engaging representation. The Avatars\' masks dynamically shift
colors, reflecting the user\'s mental state, ranging from \"Not
Stressed\" to "Overwhelmed\" (Table 7) . This user-centric and visually
intuitive approach ensures that individuals, even those with minimal
understanding of HRV metrics, can effortlessly interpret and internalize
the results.

This utility\'s significance extends beyond its technical intricacies;
it exemplifies the feasibility and efficacy of incorporating real-time
HRV analysis within a virtual environment for mental state evaluation.
The seamless integration of physiological data and user-friendly
visualizations establishes a robust foundation for future developments,
showcasing the potential for real-world applications in mental health
monitoring and virtual well-being assessments. As the experiment
progresses, this utility serves as a cornerstone, reflecting the
commitment to advancing the understanding and integration of
cutting-edge technologies in the pursuit of enhanced mental well-being.

![](vertopal_1a9b3d6962a34a55856a9ae9cc2f8552/media/image31.png){width="5.486111111111111in"
height="3.1944444444444446in"}

Figure 18: State of mind example

1.  **Conclusion**

**5.1 Summative Overview of Project Findings**

> **5.1.1 Recapitulation of Key Research Outcomes**

The culmination of this master\'s thesis has yielded noteworthy
outcomes, primarily centered around the successful implementation of the
Avatar app. Key research outcomes include the effective integration of
the HealthKit API for health data retrieval and the subsequent creation
of a dynamic and personalized avatar based on this information. The
meticulous tuning of parameters, from the avatar\'s appearance to its
behavior, showcases the versatility and adaptability of the application.
The outcomes also underscore the significance of leveraging AI-driven
avatars in health-related contexts, offering a bridge between technology
and individualized healthcare.

> **5.1.2 Insights Gained from User Interaction**

The implementation and deployment of the Avatar app provided valuable
insights into user interaction and the potential impact on
health-related behaviors. Through user testing and feedback from several
friends, it became evident that the dynamic avatar acted as a compelling
motivator, engaging users in their health and fitness journey. The
avatar\'s role in providing personalized feedback, health-related
information, and serving as a virtual companion demonstrated the
potential for fostering user adherence to health regimens. Insights
gained from user interaction contribute to the evolving understanding of
the symbiotic relationship between AI-driven avatars and individuals in
the context of health and wellness.

> **5.1.3 Implications for Health Game Development**

The implications of this project extend to the broader realm of health
game development, introducing a novel approach to user engagement and
behavior modification. The successful integration of the Avatar app with
the HealthKit API sets a precedent for future health games, emphasizing
the importance of real-time health data in creating personalized and
adaptive gaming experiences. The findings suggest that AI-driven avatars
can play a pivotal role in enhancing user immersion, motivation, and
adherence to health-related activities within gaming environments. These
implications pave the way for innovative strategies in designing health
games that go beyond entertainment, actively contributing to users\'
well-being and health management.

In conclusion, the recapitulation of key research outcomes, insights
gained from avatar implementation, and the implications for health game
development collectively contribute to a comprehensive understanding of
the project\'s significance within the broader landscape of digital
health interventions.

**5.2 Proposed Advancements**

The landscape of potential enhancements for the Avatar app is expansive,
presenting diverse opportunities to augment its functionality and user
experience. One noteworthy avenue for refinement pertains to the
fortification of the app\'s 3D architecture, leveraging the expertise of
skilled graphic designers and 3D artists. This collaborative effort
could result in a more intricate and visually captivating environment
and avatar structure, providing users with a heightened sense of
engagement and aesthetic appeal. Considerations for the incorporation of
music and sound elements into the app\'s interface emerge as a crucial
aspect of enhancing user immersion. By introducing carefully curated
auditory elements, the app can transcend its current visual-centric
nature, fostering a multisensory experience that resonates more deeply
with users during their health and fitness interactions.

In the realm of artificial intelligence, a significant advancement
involves the exploration and implementation of advanced AI models
expressly crafted to cater to the unique requirements of the Avatar app.
This initiative aims to extricate the app from external dependencies,
particularly inWorld, thereby fostering a more autonomous and
self-contained AI system. Such autonomy not only enhances the app\'s
performance and adaptability but also circumvents the need for user
subscriptions tied to external services. This strategic move ensures the
app\'s sustainability and autonomy, contributing to its long-term
viability and user accessibility.

Moreover, an ambitious plan for future iterations of the app encompasses
the expansion of health metrics derived from the iPhone\'s HealthKit.
Beyond the current set of selected metrics in the alpha version,
envisaging the integration of an extensive array of health data adds
depth and comprehensiveness to the app\'s capabilities. This expansion
could encompass a spectrum of health parameters, including but not
limited to additional vital signs, organ-specific health indicators, and
personalized wellness insights. Users would thus benefit from a more
holistic and nuanced understanding of their well-being, facilitated by
the comprehensive health data encapsulated within the app.

These potential enhancements collectively chart a course towards a more
refined, feature-rich, and user-centric avatar application. As the app
evolves, each of these envisioned advancements holds the promise of
elevating the user experience, ensuring the app\'s adaptability to
evolving technological landscapes, and positioning it as a pioneering
tool in the realm of health and fitness applications.

**5.3 Significance for Chronic Disease Management**

The Avatar app\'s contributions to Chronic Disease Management are of
paramount significance, offering innovative solutions and transformative
potential in the healthcare landscape. By seamlessly integrating with
the iPhone\'s HealthKit API, the app serves as a comprehensive health
and fitness companion, fostering a personalized and engaging approach to
chronic disease management. The incorporation of diverse health metrics,
such as heart rate, blood glucose levels, and activity data, not only
empowers users with real-time insights into their health status but also
provides a valuable tool for monitoring and managing chronic conditions.

The app\'s unique feature set, including the dynamic and visually
appealing avatar interface, facilitates a user-friendly environment for
individuals grappling with chronic diseases. The avatar serves as a
personalized guide, offering health-related information, tailored
suggestions, and motivational cues, thereby fostering a supportive and
interactive relationship between the user and the app. This dynamic
interaction aids in promoting adherence to health and fitness regimens,
a critical aspect of managing chronic conditions effectively.

Furthermore, the app\'s potential for continuous monitoring and data
retrieval aligns with the proactive management of chronic diseases.
Users can track their health metrics over time, enabling the early
detection of trends, anomalies, or deviations from baseline values. This
proactive monitoring not only enhances user awareness but also provides
healthcare professionals with valuable insights for timely interventions
and personalized treatment strategies.

In a broader context, the app contributes to patient empowerment, a
pivotal factor in chronic disease management. By offering users a
tangible and personalized tool to actively engage in their health
journey, the app fosters a sense of autonomy and accountability. This
empowerment can translate into improved self-care practices, increased
adherence to medical recommendations, and ultimately better health
outcomes for individuals managing chronic conditions.

In conclusion, the Avatar app emerges as a noteworthy and promising tool
in the realm of Chronic Disease Management, elevating the standard of
care through personalized, interactive, and data-driven approaches. Its
multifaceted contributions, from real-time health monitoring to user
empowerment, position it as a valuable asset in the holistic management
of chronic diseases, heralding a new era of technology-driven and
patient-centric healthcare solutions.

**5.4 Future Directions and Recommendations**

The successful implementation of the avatar application opens doors to
innovative future directions that can reshape the landscape of
health-related applications. One compelling avenue is the exploration of
open-world monitoring apps, wherein the user\'s health status is
dynamically reflected in an expansive virtual environment. Such apps
could extend beyond mere gamification, integrating with online games to
provide tangible rewards based on the user\'s real-world health
achievements. For instance, in an online role-playing game, maintaining
a healthy lifestyle could result in in-game gifts, bonuses, or character
enhancements, fostering a synergistic relationship between physical
well-being and virtual accomplishments. This approach leverages the
principles of positive reinforcement and intrinsic motivation, aligning
with behavior change theories to encourage sustained healthy habits.

Another promising trajectory involves the development of health-centric
games with direct online connectivity to healthcare professionals. By
establishing a seamless connection between the game and the user\'s
healthcare team, real-time health data could be shared securely,
enabling healthcare providers to monitor, analyze, and intervene
proactively. This approach transforms games into valuable tools for
health assessment and intervention, potentially revolutionizing remote
patient monitoring and extending the reach of healthcare services. The
integration of secure communication channels and adherence to medical
privacy standards would be imperative to ensure the ethical and
confidential exchange of health information between users and healthcare
providers.

Furthermore, the exploration of virtual and augmented reality (VR/AR)
applications emerges as a futuristic avenue for health and fitness
guidance. Imagine a VR or AR game that employs immersive 3D visuals and
cutting-edge technology to explain health and fitness concepts. Users
could engage with their personalized avatars, receiving visual
demonstrations and guidance in a futuristic, interactive environment.
This approach not only enhances user comprehension but also leverages
the immersive nature of VR/AR to make health education more engaging and
accessible. Incorporating evidence-based health information,
personalized to individual user profiles, ensures the scientific
accuracy and relevance of the content, contributing to the broader
landscape of health education through emerging technologies.

In conclusion, these future directions underscore the potential for
health-related applications to transcend current boundaries, offering
novel ways to motivate, educate, and connect users with their health and
healthcare providers. As technology continues to advance, the synthesis
of health, gaming, and virtual experiences holds great promise for
revolutionizing the way individuals engage with their well-being. The
ethical considerations, privacy safeguards, and scientific accuracy must
remain at the forefront of such developments to ensure responsible and
impactful integration of technology into the realm of healthcare and
wellness.

1.  **References**

\[1\] Wang Y, Wang Z, Liu G, Wang Z, Wang Q, Yan Y, Wang J, Zhu Y, Gao
W, Kan X, Zhang Z, Jia L, Pang X. Application of Serious Games in Health
Care: Scoping Review and Bibliometric Analysis. Front Public Health.
2022 Jun 10;10:896974. doi: 10.3389/fpubh.2022.896974. PMID: 35757619;
PMCID: PMC9231462.

\[2\] Clark C. Serious Games. New York: Viking Press (1970). 176 p.

 \[3\] Ritterfeld Ute, Cody Michael J, Vorderer Peter. Serious Games:
Mechanisms  and Effects. New York: Routledge (2009). 530 p.

\[4\] Beale IL, Kato PM, Marin-Bowling VM, Guthrie N, Cole SW.
Improvement in cancer-related knowledge following use of a
psychoeducational video game for adolescents and young adults with
cancer. J Adolesc Health. (2007) 41:263--79. doi:
10.1016/j.jadohealth.2007.04.006

\[5\] Karasu AU, Batur EB, Karatas GK. Effectiveness of Wii-based
rehabilitation in stroke: a randomized controlled study. J Rehabil Med.
(2018) 50:406-- 12. doi: 10.2340/16501977-2331

\[6\] Damaševičius, R., Maskeliūnas, R., & Blažauskas, T. (2023).
Serious Games and Gamification in Healthcare: A Meta-Review.
Information, 14(2), 105. <https://doi.org/10.3390/info14020105>

\[7\] Wattanasoontorn et al., 2013;Dadaczynski et al., 2023

\[8\] Haas, J.K., 2014. A history of the unity game engine.

\[9\] 1997-2009 Apple Inc. All rights reserved. Apple Inc., One Apple
Park Way, Cupertino, CA 95014, USA.
<https://developer.apple.com/documentation/healthkit>

\[10\] UModeler, Inc. 2024 UModeler X. Available from
<https://unity.umodeler.com/>

\[11\] Inworld AI 2023-2024 from
[https://docs.inworld.ai/](https://inworld.ai/)   \[12\] Wikipedia
contributors. (2023, November 19). Wii Fit. In Wikipedia, The Free
Encyclopedia. Retrieved 21:15, January 31, 2024,
from <https://en.wikipedia.org/w/index.php?title=Wii_Fit&oldid=1185934901>

\[13\] Blum A. Freestyle Libre Glucose Monitoring System. Clin Diabetes.
2018 Apr;36(2):203-204. doi: 10.2337/cd17-0130. PMID: 29686463; PMCID:
PMC5898159.

\[14\] Wikipedia contributors. (2024, January 25). Sky (video game).
In Wikipedia, The Free Encyclopedia. Retrieved 23:50, January 31, 2024,
from <https://en.wikipedia.org/w/index.php?title=Sky_(video_game)&oldid=1199066788>

\[15\] Wikipedia contributors. (2024, January 31). Journey (2012 video
game). In Wikipedia, The Free Encyclopedia. Retrieved 23:51, January 31,
2024,
from <https://en.wikipedia.org/w/index.php?title=Journey_(2012_video_game)&oldid=1201576711>

\[16\] "15 Million Merits." Black Mirror, created by Charlie Brooker,
season 1, episode 2, House of Tomorrow,
2016, <https://www.netflix.com/watch/70264858?trackId=255824129>

\[17\] 1997-2009 Apple Inc. All rights reserved. Apple Inc., One Apple
Park Way, Cupertino, CA 95014, USA.
<https://developer.apple.com/app-store/review/guidelines/>

\[18\] BeHealthKit. Dec 21 2020. Doxygen
<https://beliefengine.com/BEHealthKit/1.11/>

\[19\] Goodwill, James & Matlock, Wesley. (2015). The Swift Programming
Language. 10.1007/978-1-4842-0400-9_17.

\[20\] Microsoft Corporation, C# Language Specification Version
5.0. Available from:  \[7 June 2013\]
<http://www.microsoft.com/en-us/download/details.aspx?id=7029>

\[21\] 1997-2009 Apple Inc. All rights reserved. Apple Inc., One Apple
Park Way, Cupertino, CA 95014, USA. <https://developer.apple.com/xcode/>

\[22\] Wikipedia contributors. (2024, January 15). Gris. In Wikipedia,
The Free Encyclopedia. Retrieved 15:36, February 5, 2024,
from <https://en.wikipedia.org/w/index.php?title=Gris&oldid=1195846664>

\[23\] Wikipedia contributors. (2023, May 1). Mixamo. In Wikipedia, The
Free Encyclopedia. Retrieved 22:41, February 5, 2024,
from <https://en.wikipedia.org/w/index.php?title=Mixamo&oldid=1152595235>

\[24\] Bruneel, T. (2021, Jan 26). Measuring chronic physiological
stress with the Apple Watch and the Swift language. \[Medium\]. Tibo
Bruneel.
<https://tibo-bruneel.medium.com/measuring-chronic-physiological-stress-with-the-apple-watch-and-the-swift-language-d5f75f5a1930>

\[25\] Hernando D, Roca S, Sancho J, Alesanco Á, Bailón R. Validation of
the Apple Watch for Heart Rate Variability Measurements during Relax and
Mental Stress in Healthy Subjects. Sensors (Basel). 2018 Aug
10;18(8):2619. doi: 10.3390/s18082619. PMID: 30103376; PMCID:
PMC6111985.

\[26\] Bruneel, T. (2021, January 26). Measuring chronic physiological
stress with the Apple Watch and the Swift language. \[Medium\]. Tibo
Bruneel.
<https://tibo-bruneel.medium.com/measuring-chronic-physiological-stress-with-the-apple-watch-and-the-swift-language-d5f75f5a1930>
