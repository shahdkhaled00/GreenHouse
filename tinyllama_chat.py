from transformers import pipeline
import sys
import json

# Load the TinyLlama model
generator = pipeline(
    "text-generation", model="tinyllama-1.1b", device=-1
)  # device=-1 means CPU

# Read input from command line
user_message = sys.argv[1]

# الردود الجاهزة للأسئلة الكيميائية والزراعية
if "hello" in user_message.lower() or "hi" in user_message.lower():
    bot_response = "Hello! I'm LABventoryBot. How can I help you with chemicals or agriculture today?"

# قسم زراعة القهوة
elif "الظروف المناخية المثالية لزراعة القهوة" in user_message:
    bot_response = "مناخ معتدل دافئ بين 18-24 درجة مئوية، مع أمطار سنوية بين 1500-2000 مم، وتربة جيدة التصريف"

elif "نوع التربة الأنسب لزراعة القهوة" in user_message:
    bot_response = (
        "التربة البركانية أو الطميية العميقة، الغنية بالمادة العضوية وذات pH بين 6 و6.5"
    )

elif "تحسين خصوبة التربة وزيادة الإنتاجية" in user_message:
    bot_response = "باستخدام السماد العضوي، تدوير المحاصيل، زراعة النباتات المثبتة للنيتروجين، وتحليل التربة دوريًا"

elif "تحليل التربة قبل الزراعة" in user_message:
    bot_response = "يساعد في تحديد خصائص التربة من حيث pH والعناصر الغذائية، مما يوجه عملية التسميد والري بدقة"

elif "علامات نقص النيتروجين في نبات القهوة" in user_message:
    bot_response = "اصفرار الأوراق السفلية، ضعف النمو العام، وانخفاض الإنتاج"

elif "مواجهة ملوحة التربة أو المياه في زراعة القهوة" in user_message:
    bot_response = "باستخدام مياه قليلة الملوحة، تحسين الصرف، زراعة أصناف مقاومة، وغسل الأملاح بالماء النقي"

elif "أفضل طرق الري المستخدمة في مزارع القهوة" in user_message:
    bot_response = (
        "الري بالتنقيط أو الري بالرش، لأنها تقلل الفاقد وتحسن كفاءة استخدام المياه"
    )

elif "كم مرة يجب ري نبات القهوة" in user_message:
    bot_response = (
        "يعتمد على الطقس ونوع التربة، لكن عمومًا مرة إلى مرتين أسبوعيًا خلال فترات الجفاف"
    )

elif "الوقت المثالي لزراعة شتلات القهوة" in user_message:
    bot_response = "بعد انتهاء موسم الأمطار أو في بدايته، لضمان توفر رطوبة كافية"

elif "الكمية المناسبة من السماد العضوي للقهوة" in user_message:
    bot_response = "حوالي 20-30 كجم لكل شجرة سنويًا، تُقسم على دفعتين"

elif "أهمية عنصر البوتاسيوم لنبات القهوة" in user_message:
    bot_response = "يساعد في تحسين جودة الحبوب، وتنظيم امتصاص الماء، ومقاومة الأمراض"

elif "نقص الحديد في نبات القهوة" in user_message:
    bot_response = "يظهر الاصفرار بين عروق الأوراق الحديثة، مع بقائها خضراء جزئيًا"

elif "التسميد الورقي للقهوة" in user_message:
    bot_response = "هي رش العناصر المغذية على الأوراق، وتناسب القهوة لتعويض العناصر بسرعة عند النقص"

elif "احتاج القهوة إلى الكالسيوم" in user_message:
    bot_response = "نعم، فهو ضروري لتكوين الجدر الخلوية ونمو الجذور وتماسك الثمار"

elif "الفرق بين التسميد العضوي والتسميد الكيميائي للقهوة" in user_message:
    bot_response = "العضوي يحسن خصائص التربة على المدى الطويل، بينما الكيميائي يعطي نتائج سريعة ولكن قد يضر التربة مع الوقت"

elif "الآفات شيوعًا التي تصيب نبات القهوة" in user_message:
    bot_response = "حفار ساق القهوة، البق الدقيقي، والمنّ"

elif "مرض الصدأ البرتقالي في القهوة" in user_message:
    bot_response = (
        "مرض فطري يصيب الأوراق، يسبب بقعًا برتقالية ويسبب تساقطها وضعف الإنتاج"
    )

elif "مكافحة حشرة حفار ساق القهوة" in user_message:
    bot_response = (
        "باستخدام المصائد الفيرمونية، والمبيدات الحيوية، وتنظيف الجذوع المصابة"
    )

elif "المبيدات العضوية في مزارع القهوة" in user_message:
    bot_response = "نعم، مثل زيت النيم أو مستخلصات الثوم والفلفل الحار"

elif "النظافة الزراعية في الوقاية من الأمراض" in user_message:
    bot_response = "تقليل العدوى بقطع الأجزاء المصابة ودفنها، وتخفيف الرطوبة الزائدة"

elif "احتاج القهوة إلى التظليل" in user_message:
    bot_response = (
        "نعم، خاصة في المناطق الحارة، لتقليل الإجهاد الحراري وتحسين جودة الحبوب"
    )

elif "الأشجار المناسبة لتظليل القهوة" in user_message:
    bot_response = "أشجار مثل الغاف، الغَرَب، أو الموز لأنها توفر ظلًا خفيفًا وتخصب التربة"

elif "تأثير الرياح على نبات القهوة" in user_message:
    bot_response = "نعم، الرياح الشديدة قد تكسر الفروع أو تسبب فقدان الأزهار والثمار"

elif "تقليل تأثير البرد أو الصقيع" in user_message:
    bot_response = (
        "زراعة القهوة في مناطق مرتفعة معتدلة، استخدام أنظمة تغطية، أو التظليل الكثيف"
    )

elif "تأثير الأمطار الزائدة على القهوة" in user_message:
    bot_response = "قد تؤدي إلى تعفن الجذور وانتشار الفطريات، لذا يجب تحسين الصرف جيدًا"

elif "إنتاج القهوة بعد الزراعة" in user_message:
    bot_response = "بعد حوالي 3 إلى 4 سنوات من الزراعة، حسب الصنف والرعاية"

elif "علامات نضج ثمار القهوة" in user_message:
    bot_response = "تغير اللون إلى الأحمر أو الأصفر حسب الصنف، وسهولة الفصل عن العنق"

elif "الفرق بين المعالجة الرطبة والجافة لحبوب القهوة" in user_message:
    bot_response = "الرطبة: إزالة اللب والتخمير قبل التجفيف. الجافة: تجفيف الثمرة كاملة"

elif "طريقة المعالجة على نكهة القهوة" in user_message:
    bot_response = (
        "المعالجة الرطبة تعطي طعمًا أنظف وحمضيًا، والجافة تعطي نكهة فاكهية قوية"
    )

elif "توقيت الحصاد على جودة القهوة" in user_message:
    bot_response = "نعم، الحصاد المبكر أو المتأخر يقلل من الجودة والنكهة"

# قسم الأسئلة العامة باللغة الإنجليزية
elif "ideal soil conditions for most crops" in user_message.lower():
    bot_response = (
        "Loamy, well-drained soil with good fertility and a pH between 6.0 and 7.0"
        
    )

elif "soil fertility be improved and maintained" in user_message.lower():
    bot_response = "By adding organic matter, practicing crop rotation, using compost, and applying balanced fertilizers"

elif "soil analysis important before planting" in user_message.lower():
    bot_response = "It helps determine nutrient levels, pH, and soil structure, which guide fertilization and crop choice"

elif "signs of nitrogen deficiency in plants" in user_message.lower():
    bot_response = "Yellowing of older leaves, stunted growth, and pale green color"

elif "reduce water consumption in irrigation" in user_message.lower():
    bot_response = "By using drip irrigation, scheduling irrigation wisely, and mulching to retain moisture"

elif "best irrigation method for coffee plants" in user_message.lower():
    bot_response = "Drip irrigation, as it conserves water and delivers moisture directly to the roots"

elif "soil salinity be managed" in user_message.lower():
    bot_response = "By flushing the soil with fresh water, using salt-tolerant crops, and improving drainage"

elif "benefits of crop rotation" in user_message.lower():
    bot_response = (
        "It improves soil health, reduces pest cycles, and balances nutrient use"
    )

elif "best time to plant coffee seedlings" in user_message.lower():
    bot_response = (
        "At the beginning or end of the rainy season, when moisture is adequate"
    )

elif "coffee trees to start producing beans" in user_message.lower():
    bot_response = "Typically 3 to 4 years after planting"

elif "type of climate is best for coffee cultivation" in user_message.lower():
    bot_response = "A tropical climate with temperatures between 18°C and 24°C, and rainfall around 1500-2000 mm per year"

elif "pH important in soil health" in user_message.lower():
    bot_response = "It affects the availability of nutrients and the activity of beneficial microorganisms"

elif "mulching and why is it useful" in user_message.lower():
    bot_response = "It's covering the soil with straw or leaves to conserve moisture and reduce weed growth"

elif "overwatering affect plant roots" in user_message.lower():
    bot_response = "It can cause root rot and reduce oxygen availability, leading to poor plant health"

elif "organic farming" in user_message.lower():
    bot_response = "A method of farming without synthetic chemicals, using natural fertilizers and biological pest control"

elif "role of phosphorus in plant growth" in user_message.lower():
    bot_response = (
        "It supports root development, flowering, and energy transfer in the plant"
    )

elif "farmers identify potassium deficiency" in user_message.lower():
    bot_response = "Leaf edges turn brown and curl, and plants may become weak and more disease-prone"

elif "coffee plants benefit from foliar feeding" in user_message.lower():
    bot_response = "Yes, especially during nutrient deficiencies, as leaves absorb nutrients quickly"

elif "advantages of using compost in farming" in user_message.lower():
    bot_response = "It improves soil structure, fertility, and water retention, and adds beneficial microbes"

elif "difference between organic and chemical fertilizers" in user_message.lower():
    bot_response = "Organic fertilizers release nutrients slowly and improve soil health, while chemical ones provide quick results but may harm the soil long-term"

elif "common pests that attack coffee plants" in user_message.lower():
    bot_response = "Coffee berry borer, leaf miners, and mealybugs"

elif "farmers prevent plant diseases naturally" in user_message.lower():
    bot_response = "By using resistant varieties, rotating crops, maintaining field hygiene, and avoiding overwatering"

elif "integrated pest management (IPM)" in user_message.lower():
    bot_response = "A strategy combining biological, cultural, physical, and chemical tools to control pests sustainably"

elif "pruning help in crop production" in user_message.lower():
    bot_response = (
        "It improves air circulation, light penetration, and stimulates new growth"
    )

elif "impact of using too many pesticides" in user_message.lower():
    bot_response = "It can harm beneficial insects, lead to resistance, and contaminate soil and water"

elif "signs that coffee cherries are ready to harvest" in user_message.lower():
    bot_response = "They turn bright red (or yellow for some varieties) and are easy to detach from the branch"

elif "difference between wet and dry coffee processing" in user_message.lower():
    bot_response = "Wet processing removes the pulp before drying; dry processing dries the whole cherry"

elif "farmers improve crop quality after harvest" in user_message.lower():
    bot_response = "Through proper drying, sorting, storage, and timely transportation"

elif "factors affect the flavor of coffee" in user_message.lower():
    bot_response = (
        "Soil type, altitude, variety, processing method, and roasting conditions"
    )

elif "proper storage important after harvest" in user_message.lower():
    bot_response = "To prevent mold, pest attacks, and loss of quality or nutrients"

else:
    # Add a system prompt to make it act like a chatbot
    prompt = f"System: You are LABventoryBot, an expert in chemistry and agriculture. Provide a detailed and accurate answer about chemical properties, agriculture, or coffee farming based on your knowledge. If the user asks in Arabic, respond in Arabic; otherwise, respond in English. If you don't know, say 'I don't know.' User: {user_message} Assistant: "

    # Generate response
    response = generator(
        prompt,
        max_length=150,
        num_return_sequences=1,
        do_sample=True,
        temperature=0.4,
        truncation=True,
    )[0]["generated_text"]

    # Remove the prompt part from the response
    bot_response = response.replace(prompt, "").strip()

    # Clean up any leftover "User:" or "Assistant:" parts in the response
    bot_response = bot_response.split("User:")[0].split("Assistant:")[-1].strip()

# Output response as JSON
print(json.dumps({"response": bot_response}))
