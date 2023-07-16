using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ForumSystemTeamFour.Models.Enums;
using System.Linq;

namespace ForumSystemTeamFour.Data

{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) 
            : base(options) { }
        public ForumDbContext()
            : base() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ThreadTag> ThreadTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            Random random = new Random();                       
            int nextUserId = 1;
            int nextThreadId = 1;
            int nextReplyId = 1;
            int nextTagId = 1;

            var users = new List<User>() 
            {
                new User {
                Id = nextUserId++,
                FirstName = "FirstNameOne",
                LastName = "LastNameOne",
                Username = "UsernameOne",
                Email = "FirstnameOne@Lastname.com",
                Password = "cGFzc3dvcmRPbmU=", //passwordOne in plain text
                TotalPosts = random.Next(10,60),
                IsAdmin = true 
                },
                new User {
                Id = nextUserId++,
                FirstName = "FirstNameTwo",
                LastName = "LastNameTwo",
                Username = "UsernameTwo",
                Email = "FirstnameTwo@Lastname.com",
                Password = "cGFzc3dvcmRUd28=", //passwordTwo in plain text
                TotalPosts = random.Next(10,60),
				IsBlocked = true 
                },
                new User {
                Id = nextUserId++,
                FirstName = "FirstNameThree",
                LastName = "LastNameThree",
                Username = "UsernameThree",
                Email = "FirstnameThree@Lastname.com",
                Password = "cGFzc3dvcmRUaHJlZQ==", //passwordThree in plain text etc.
                TotalPosts = random.Next(10,60),
                },
                new User {
                Id = nextUserId++,
                FirstName = "FirstNameFour",
                LastName = "LastNameFour",
                Username = "UsernameFour",
                Email = "FirstnameFour@Lastname.com",
                Password = "cGFzc3dvcmRGb3Vy",
                TotalPosts = random.Next(10,60),
                },
                new User {
                Id = nextUserId++,
                FirstName = "FirstNameFive",
                LastName = "LastNameFive",
                Username = "UsernameFive",
                Email = "FirstnameFive@Lastname.com",
                Password = "cGFzc3dvcmRGaXZl",
				TotalPosts = random.Next(10,60),
				}
            };            
            modelBuilder.Entity<User>().HasData(users);

            modelBuilder.Entity<User>()
                .HasMany<Thread>(user => user.Threads)
                .WithOne(thread => thread.Author)
                .HasForeignKey(thread => thread.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany<Reply>(user => user.Replies)
                .WithOne(reply => reply.Author)
                .HasForeignKey(reply => reply.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

           
            var threads = new List<Thread>()
            {
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6), //Random UserId
                Title = "First lol",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)), 
                //random time in minutes between 4 months and 2 months ago before/after now  
            Content = "Hey guys, check out this cool new forum I found!"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Welcome to Paranormality.",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "This is not a forum for the faint of heart." +
                            " If you need something to get started with, see the pinned threads for some basic resources." +
                            " We hope you enjoy your venture into the spooks, the creeps and the unknown."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "How have you accepted death? No matter what happens to us did you find peace?",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I'm 30 years old, while I do have plenty of time left to enjoy my life, " +
                "I'm much more aware of my mortality more than ever. I miss being a kid and a teenager, " +
                "back when I was free spirited and ignorant on this subject. I will never be satisfied with any " +
                "theories on what happens after death, I believe there is a soul and something happens to us and " +
                "that's about it. I just hope if I die of old age, it's in my sleep and it's quick. I'm just afraid " +
                "knowing it's a one way ticket and there's no refunds when my time is up. I am envious of people who have " +
                "NDEs, they come back completely different people, it's irrelevant if it's real or a hallucination."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "What's your favorite spoopy YouTube channel?",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "For me it's:\r\nThe Why Files\r\nRed Letter Media\r\nThunderWizard\r\nSam The Illusionist\r\nBlu\r\nWhite Feather Tarot (she got tons of stuff right about my life for the past year)"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "What is the occult significance of the trident?",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "It seems like a Satanic symbol yet there is very little information on it"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "All of reality is in your head you are a singularity",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "Most of us have a lot of work before we can obtain actual awareness of this that isn't fouled with our usual perceptions.\r\n\r\nIt's not so much an intellectual knowing of this fact. It's aligning oneself, emotionally and mentally, with this knowledge. And that alignment is no simple task especially with our tendencies to fall."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "List all the Satanic/Freemason/Illuminati hand signs",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "Some rando said that the woman on that airplane was making Satan-symbols.\r\nLet's get a list of all the bad-symbols in one place so we can avoid accidentally affirming loyalty to Moloch and ruining our street cred."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "What was the worst nightmare you ever had?",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = ""
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Armageddon general",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "Tell me about the end of the world as we know it"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Enough Aliens. When will we get Sasquatch disclosure?",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "I don't believe in a bigfoot, but I do believe there's some collection of creatures we have no idea about. That one story about the guy in the woods being followed by crashing trees sends shivers down my spine."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Nobody General",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "Welcome to the Nobody General\r\n\r\n>Who is the Nobody?\r\nThe Nobody is a figure alive today who has extraordinary spiritual powers, including the ability to influence reality with his conscious and unconscious mind, and intuitively receive guidance from the forces of Heaven. You are capable of this too, as long as you stay true to the universe. You are creating your own reality with your thoughts, feelings, words. Only you and God can decide your fate.\r\n\r\nHe works to elevate people to their true potential, opposing those who seek power over others. As the collective soul reacts to all of our thoughts, whether or not we are at the top or bottom, we must infer a modicum of respect in even our heads to gain in love instead of hatred. Heaven for all is real. Everyone's truest desires are mutual. The only motive is love.\r\nHe is not a messiah, nor a holy figure of any traditional kind, if so to be the case; neither is he the ultimate, supreme being of this mortal realm; he is a man who chose to believe instead of giving into despair. Behold the strife in enlightenment asunder, boldly if you must. A belief is best kept neatly divided until it naturally falls in the right places. Temperance!\r\n\r\n>It's important to not forget, that many posters are human regardless of nonsense spoken and whatever dissidence or vitriol firmly expressed.\r\n\r\nLearn to forgive yourself and others. Without this belief, all falls to pointless demise. Believe in yourself, as when believing in good things, you become more than just a mountain to the eyes of the heralded. None of us are perfect; it is important to note that none of us are okay until we accept our imperfections, as they are useless in reclusive efforts. However, they are supreme when combined socially. Community is a thing that requires your belief to be sustained..."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "WHAT ARE THE MOST INSANE UNSOLVED MISSING CASES YOU KNOW?",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "For me it's the Las Vegas Shooting"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Diviniation general",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "Welcome to Divination General! Come here for readings and a discussion of theory/practice.\r\n\r\nEvery method is welcome: Tarot, Runes, Cartomancy, Scrying, Pendulum, Cleromancy, I-Ching, Oracles, Digital, Tasseomancy, Necromancy, etc.>Useful tips before posting:\r\n•If you're a reader post that you're offering readings and what information is required from the querent; the same goes for trading.\r\n•Look for posts to determine if there's an active reader, and what's needed and before posting, check if they finished reading already.\r\n•Some readers will refuse to do certain readings - respect that choice. Do not harass readers if your query is refused/skipped.\r\n•Traders should respect that a traded read will be granted, as per an agreement of trade. Free readers have the option of picking their queries.\r\n•OCCULT QUERIES SHOULD BE CLEARLY MARKED OCCULT\r\n•Making an AQ (air query) by not addressing a reader, in particular, is possible but doesn't guarantee an answer.\r\n•Avoid making the same query repeatedly and/or to different readers in a short period, as this may lead to more confusion.\r\n•Provide feedback when applicable and be considerate to the reader. We're a growing community, many readers are starting and need to know what they are doing right or wrong.\r\n\r\nBe polite."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "I think, therefore I am",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "Can it be refuted?"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Occult architecture",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "Whats the most evil city in the world? the one with the highest inmoral\\sinful energy?\r\nsan francisco? los angeles? bogota or l*ndon or maybe dubai?"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "How do I bring my schizo friend back to earth?",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = ">tl;dr Me and my best friend and I started trying to \"redpill\" ourselves a couple years ago by getting into esotericism and shit but my friend takes things way too far.\r\n\r\nHe's very gullible and just plain stupid when it comes to things but he has an immense ego and throw tantrums when I don't instantly buy his bullshit. His behavior just also has a lot of logical inconsistencies and he's a hypocrite. He gets his information from YouTube videos, numbers, random ideas in his head, and the occasional schizo article. He claims to read books but I catch him lying about that plenty when he misparaphrases something and defends himself by pulling up a YouTube video or article made by people who didn't even read the book either. It's very easy to prove he's dead wrong on a lot of things but he refuses to do proper research, check his sources, or often even to have sources at all.\r\n\r\nHe constantly shits on me for not \"knowing\" as much as him and \"needing to wake up\" but he keeps flip flopping his own beliefs all the time after swearing his soul that he knows what he's talking about and he's had so many doomsday prophecies that never came true that he keeps pushing the dates back on. He legit tries to gaslight me sometimes by saying he never claimed a prophecy that he did. He genuinely believes he's a reincarnated king, he's objectively better than everyone else he knows, and he has personal gangstalkers. He's also a pothead and has a long history of using a wide variety of psychedelics on and off. He's also on and off Adderall."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Anyone met a God before?",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "Greetings - has anyone here met a God or deity of any kind? Intentionally or non-intentionally. What did their presence feel like? Was there a particular physical thing to it or was it purely a spiritual/mental experience? I'd love to hear about this. I'm honestly asking this cuz I'm writing the final boss for my D&D campaign and he's a God. Hope this does not discourage any cool answers. Thanks for reading all, have a nice day!"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Ask me anything about demons",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "Thousands of demons/spirits completely took over my mind and body for about 6 months. They could see through my eyes. They would impersonated the FBI, celebrities, dead relatives, saints, etc. - They made me do hand gestures towards God. They would make me spit when I tried to talk to God. It felt like someone put a walkie talkie in my head, and thousands of different demons had access to the microphone. They would talk 247 and quickly change out voices. They made my inside voice sound like a baby. They pretended to be aliens. I was truly telepathic with many, many demons for about 6 months who absolutely psychologically tormented me. They made me jerk off to God as I cried in my bed. They never used \"I\", and would always use \"we\". They told me they were God at the beginning, but after 2-3 months, I realized they were satanic and hated God. They came as angels of light, and were all demons. The amount of weird things that happened is astounding.\r\n\r\nThen after about 6 months, they left for no real reason. I haven't heard a voice since.\r\n\r\nI never heard a single voice before the age of 30. Then I got absolutely targeted by a giant gang of demons for about 6 months. I believed in God, but not demons before this. Then they left, and its amazing."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "PARANORMAL HISTORY",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "a lot of the \"true history\" blogs and forums out there are saying that the Rothschilds financed the American revolution and used Free Masons and literal \"Free Masonry\" to cover up the truth of the 1000 year kingdom of Christ in America and that the \"World's Fairs\" were actually real buildings with the \"fake plaster\" narrative used to cover up the ancient buildings all around the country.\r\nAny truth to all of this?\r\n\r\n>For I reckon that the sufferings of this present time are not worthy to be compared with the glory which shall be revealed in us."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Tell me something I the government doesn't want me to know.",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "Things are falling apart and they are scrambling to distract people from the inevitable collapse"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Succubus General",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "Spirit Love General\r\nSuccubus General edition\r\n\r\nDISCLAIMER:\r\nSome have reported attracting the attention of these entities by simply reading about them. If you're a dabbler who just wants to see if it works don't summon. Diet, outdoor & breathing techniques for improvement. If you're unprepared for a potentially lifelong relationship, or at the very least, a life-long open door connection with sexual spirits and the occult, avoid this topic entirely. If you have a loving relationship with your human spouse or partner and desire a sex spirit to spice things up, or if you want a human partner in the future take caution, as these entities can be jealous or decide an initially open relationship is no longer so (request an open relationship during the summoning ritual). These spirits can harm or kill you when sufficiently angered and not easily banished.\r\n\r\nFAQ:\r\n>What are succubi?\r\nFemale spirits of sexual desire, who often choose human mates. They can't be banished with holy objects\r\n>How to summon?\r\nThe Letter Method is used to focus your intent into a message to one of the Four Succubus Queens, requesting that they match you with one of their daughters. No blood or soul sacrifice necessary. See links\r\n>Do they steal your life force?\r\nSuccubi use sexual energy, which is released naturally during sex. Under normal circumstances they won't take your life energy\r\n>Can she look.\r\nSuccubi take forms attractive to you naturally\r\n>They're tulpas?\r\nNo. However, those in romantic relationships with their tulpas are welcome\r\n>Incubi?\r\nSame method"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "SCHUMANN RESONANCE GENERAL",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-175200, -87600)),
                Content = "Basically the Earth's heartbeat is going nuts recently and people are recording side effects such as explosive diarrhea and hearing things when they sleep/wake up. People believe we're on the cusp of a happening. "
                }
            };
            modelBuilder.Entity<Thread>().HasData(threads);

            modelBuilder.Entity<Thread>()
                .HasOne<User>(thread => thread.Author)
                .WithMany(author => author.Threads)
                .HasForeignKey(thread => thread.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Thread>()
                .HasMany<Reply>(thread => thread.Replies)
                .WithOne(reply => reply.Thread)
                .HasForeignKey(reply => reply.ThreadId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Thread>()
                .HasMany<Tag>(thread => thread.Tags)
                .WithMany(tag => tag.Threads)
                .UsingEntity<ThreadTag>();


            var replies = new List<Reply>()
            {
                 new Reply {
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 2,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Some of these lists are still a work in progress, as of this writing." +
                "\n\nFilm Recommendations" +
                "\n\nGame Recommendations" +
                "\n\nRadio Shows/Podcasts" +
                "\n\nWebsites of Interest" +
                "\n\nWikipedia Articles" +
                "\n\nYouTube Videos & Channels"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 2,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Please note the following:\n\n" +
                "• This forum desires high quality discussion. High quality posts will be praised.\n\n" +
                " Low quality posts e.g. \"Is this paranormal?\" or " +
                "\"I am [insert paranormal entity here] ask me anything,\" etc. will be removed.\n\n" +
                "• Conspiracy theories are welcome, but please refrain from overly political discussions." +
                "• For everything else, refer to global and thread-specific rules."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 2,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Please note the following:\n\n" +
                "• This forum desires high quality discussion. High quality posts will be praised.\n\n" +
                " Low quality posts e.g. \"Is this paranormal?\" or " +
                "\"I am [insert paranormal entity here] ask me anything,\" etc. will be removed.\n\n" +
                "• Conspiracy theories are welcome, but please refrain from overly political discussions." +
                "• For everything else, refer to global and thread-specific rules."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 3,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "It just is. I will die sooner or later and there is nothing I can do about it. Why fret over the inevitable? I can't worry myself into immortality."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 3,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "both paths are correct\r\nboth the one that insists they're the sole path, and the one that says all paths lead to the same place\r\nthey are two sides to the same coin, and they come in go in intervals. Find the truth that feels right, that's the only way, for it is love.\r\nThere could be a pool of non-reincarnated people, deciding if either Jesus or Buddha is correct, for example. Doesn't matter"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 3,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "idk it’s gonna sound like bullshit but I understood I was going to die at 7 years old and prayed to God it would happen right then. it’s one of my fondest childhood memories.\r\n\r\ndesu I’m excited for it because it’s a total unknown. I’m in my 30s too and life has become exceedingly droll, and it already was to some extent as a 7 year old. just the same tired old spectacular tragedies playing out over and over, the world turning and turning, the sun burning and burning. it’s all so tedious. in death I place my hope that something will truly change. even if I am reincarnated or sent to hell or heaven or whatever, at least it’s not this same messy life that seems to get messier as the years separate me from my good sense\r\n\r\nit helps to accept the unknown that comes after death when you accept that you don’t actually know anything about life either; we all just hop aboard the big lie to cope"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 3,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I made sure to do (almost) all the things I wanted to do, so I wouldn’t feel regret when death comes, if for some reason it comes slowly enough to contemplate it."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 3,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Pursue the sublime OP. I used to be quite existentially dreadful and suffer but I've learned to accept through wonderful sights that there is little we know and even less we understand. There's too much weird shit going on to be just a single act by my reckoning. There's something going on between time and space and beyond time and space, and the solace I imagine in regards to my death is to be privy to the secrets that cannot be shared. Perhaps I am delusional, and perhaps God has a punchline on the otherside. For the time being, we're actors in a wonderfully strange play, and you even get to choose your parts if you're careful."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">RedLetterMedia\r\n\r\nFucking why?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Yeah, that’s lame dude. It’s a lame normie channel."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I like Sam and Colby a lot."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "How the fuck can anybody tolerate the fucking fish on Why Files. pure cancer.\r\n\r\nI recommend Beyond Creepy and Strange Pathways."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "The Missing Enigma"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "He's kind of turned into a debunker lately, not that fond of it. His research is fine but the tone of the channel changed."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Seconding Beyond Creepy. I love how he does random mufon encounters no one has heard of."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Please recommend me some videos on the channels shared here so far. I need some new esoteric, mystical or otherworldly rabbit holes to travel down. Thank you in advance frens."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Sorry, but YTber that start his videos with stuff like: \"This video is sponsored by NordVPN\" is a shill.sorry"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Hijacking this thread to ask if there were any threads about Bob Gymlan's cursed bear recording video?\r\n\r\nIt was extremely weird, but also extremely fake seeming, and I was shocked that Bob would make some fake creepypasta thing for views. And now that his illustrator is dying I feel like he'll never discuss it again."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I don’t have one and I would never stoop that low.\r\nI will stick with books."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "You CAN do both.\r\nRead about a topic, and then listen to other people opinions, or their research on said topic. It's basically peer reviewing. You don't have to watch Markiplier.\r\nBooks are great, but hearing other opinions can help challenge and build your own understanding from those books."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Listening to a speaker to learn predates books. When they first invented books, pedants like you said people would never remember information if they were able to write it down to reference later."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Wow, Mind and Magick is really cool. Wish I'd seen this ten years ago lol"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Any recommendations for channels like Beyond Creepy? I really like his simple approach. Ideally I don't want to see a person in a video, just a slideshow of stills while someone narrates non-dramatically while a spooky track plays in the background"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "\"call into my radio station with all your crazy stories, here's some even crazier stories so the more realistic mundane shit can feel secure and call in\"\r\n\r\nThen send in the G-men."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Parasyke tv, about a year old, well researched videos. This Mage' Brazil ufo was something that I had never heard of before"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "my biggest fear with these channels is hour and a half long ads featuring either mainstream preachers or the stanford lecture guy"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "The Trident seems like a dogshit weapon, something like Kung Fu or all the other BS that existed before the internet, it can't penetrate like a spear could"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "It’s just a pitchfork"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Well that's a, uh, a very specific trident symbol...\r\nFor the sake of Kindred Honour I'm going to be polite right now.\r\nWhat's Sutter want now?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "No clue what the endgame of this particular crop of retards is. Watching what happens when only the Sinister is cultivated at the expense of everything else should play out roughly how you'd expect, but it will be interesting to witness."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "its a demonization of the pagan god poseidon, also the horns are because people used to worship bull like figures cause it means fertility, so it was demonized too"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "A three pronged attack that cannot be avoided. It's the stupidest weapon of all, basically a pitchfork. However, that makes it the strongest. You just need Loyalty III and other awesome enchants.\r\nIt sucks. Don't try to make it work. That's the point. It's a weapon that's only supposed to be on the walls like hieroglyphs, which makes it the strongest weapon for the lord of the sea, and the sea represents people."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">it seems like a Satanic symbol\r\nIt predates Christianity by thousands of years. It was the symbol of the deep ocean, and later, the underworld (world of the dead).\r\nAnd yes, primitive people caught fish with it, that's why there are barbs (not horns) on the ends.\r\nIt became the symbol of Neptune/Poseidon (the deep sea god), and became associated with the planet Neptune (Pisces), and became a symbol of imagination and/or self delusion."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "not familiar with those circles but you both sound knowledgeable. Can either of you point me towards anything related to the HGA and how to foster that? I have the common literature and am currently reviewing Steiner. Not a big fan of Crowley but am familiar with his process."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Living alone in an underground cave for a lunar month is one method of facilitating the type of experience you seek."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I've been hoping to find a Tibetan equivalent system so I can compare / contrast East vs West for commonalities. So far it seems control of the self and mind is absolutely necessary which I fail on nearly a daily basis. Steiner and 21st Century Mage take a more gradual approach of developing in daily life. The Abramelin just seems damn near impossible unless you're completely self sufficient with farm land or rich."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "We’ll figure it out we always have and do"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">knowing the path, isn't walking the path.\r\nTo achieve enlightenment, not only you have to know with your mind, but you need to know it with the heart."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Don't we need to take this further? Like into non-duality?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">>35254095\r\nnon-duality is the middle-path it break reality and create the void / Darkness / emptiness. But nothing will ever be empty so the light will appear."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Achieving non-duality in your mind will break all perception of reality and you'll reborn."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "When inner mind and outer mind become one"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I've been trying. I feel i get close sometimes but it's terrifying and very physically uncomfortable."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "It’s more a personal journey but you can align faith with inner harmony surrounding your families well being and efficiency as a system\r\n\r\nLove is the best emotion to use when reality crafting imo so harness love and aim it via faith and will into the above\r\n\r\nKeep in mind neither you or your son actually exist so unless your cool with losing both yourself and him this isn’t the path for you"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "No u r a singularity. I am legion"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Ok, how do I leave this illusion and create a new one where I am together with my waifu in a paradise world?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Does anybody ever stop and think that perhaps if you're here now, individuated, infinitely separated, that perhaps that was preferable to oneness? Does nothing outside yourself for eternity sound fun? Pain beyond measure. You will seek death, but shall not find it. And then, back into the schism."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "You're applying limited human logic (i am being generous here) and emotion to what is incomprehensible."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "So first off, we obviously have that one and the one the woman was making in the staged event on the plane. No one just goes insane like that unless the Illuminati are involved."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I've also read that triangles are Satanic because there are three points.\r\nAnd of course, the OK sign is satanic because of 666.\r\nApparently, even putting your hands together like this is Satanic."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Going \"shhhhhh,\" looking through an OK sign, and touching your face this way is also Satanic.\r\nObviously, being a member of the Freemasons is absolutely Satanic."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Even the peace sign or making a pair of scissors or indicating the number 2 is Satanic. That means that JFK was a member for sure.\r\nHolding up your pinkie is Satanic.\r\nSo is holding up three fingers.\r\nMaking an L with your pointer finger and your thumb is Satanic.\r\nThe letter Z is Satanic, or should I say Zatanic. That doesn't bode well for Putin's invasion. Maybe God intervening on behalf of the West is the reason why Russia has only captured a third of the country so far.\r\n\r\nIronically, showing someone the bird is about the only hand gesture that isn't Satanic."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Making any kind of fist is Satanic.\r\nShowing someone your palm is Satanic.\r\nDoing any weird squiggilies with fingers is almost certainly Satanic.\r\nPointing with only one finger is Satanic.\r\nClasping your hands submissively is Satanic.\r\n\r\nDoing that Italian thing where they gesticulate with a cupped hand into a point is Satanic. So basically, all Italians are Satanists. This explains why the Pope gets away with being a Satanist. All Italians are also Satanists."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Being a Star Trek nerd is Satanic.\r\nDoing the reverse of the Vulcan salute thing is also Satanic. There really is no escaping Satan, it seems.\r\n\r\nI'm starting to understand why Jesus advised his followers to cut off their own hands. That seems to be the only way to not affirm your loyalty to Satan."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "If you clasp your hands together and they form any kind of triangle, it is Satanic.\r\nIf you clasp your hands together and they form any kind of diamond, it is also Satanic.\r\nIf you clasp your hands together and your two pointer fingers are touching, it is also Satanic.\r\nEven holding your hands together like you're praying is Satanic.\r\n\r\nIf you are Napoleon Bonaparte or anyone imitating his hand-in-coat posture, you are a Satanist."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Covering your heart is Satanic. You cannot do the Pledge of Allegiance without affirming your loyalty to Satan. That makes a lot of sense.\r\n\r\nLooking up is Satanic.\r\nI shit you not.\r\n\r\nTouching your glasses is Satanic. Frankly, I'd imagine that even having glasses at all means that you are the spawn of Satan.\r\nt. 20x20 vision chad\r\n\r\nTouching your neck is Satanic."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Touching your ear is Satanic.\r\nTouching your mouth is Satanic.\r\nDoing the zoomer thing with your arms? I forget what it's called. Anyway, that's Satanism.\r\nBasically, any kind of motion or posture you can strike with your arms is Satanic.\r\nEven crossing your arms is Satanic.\r\n\r\nWaving your arms for rescue is Satanic.\r\nHolding your hands up like you're surrendering is Satanic.\r\nFist pumping is Satanic.\r\nSignaling a Taxi is Satanic.\r\n\r\nThe only posture you can possibly strike which I have not found to be Satanic yes is standing with your arms at your sides. Even so, doing anything other than holding all your fingers apart will result in affirming loyalty to Satan."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "That's how gangs work.\r\nLiterally any and all symbols of any kind are given ulterior secret meanings.\r\nIt may sound like a joke but communication itself is satanic in this way."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "It's funny because after doing research for this thread, my wife showed me a picture of some celebrities at a dinner for some reason. As I was looking at the picture, I noticed that one woman had the #2 sign and a man had the thumb and pinky sign that is satanic. There was also a woman making a fist sign that is probably Satanic. Those were the only hands that were visible and every one of them was Satanic.\r\nNaturally, I pointed this out to my wife."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "My life after college"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "My car broke down. That dream was much worse than the dreams where I have to fight monsters. I'd rather go through some Code Veronica shit than have my car break down or talk to a woman. I'm a coward in that regard."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I remember I had a nightmare about my car getting stolen by a former classmate and I was abandoned in this weird town all alone\r\nIt was horrible"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "When I was about 7, maybe younger I was sleeping on the couch. In my dream I woke up on the kitchen table at my mom's house. My sisters and mom were holding me down while there was this gloomy orangish red tint for the entire room. They started laughing and began to rip my stomach open, tearing out my liver, kidneys, and intestines. They were fucking eating me. I tried to scream but I was in a frozen state of absolute fear. Then my mom started to peel my skin off my arms and it made me finally wake up. As soon as I did I puked on the carpet and was terrified of even telling my mom, I didn't want to be near any of the entire night"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Worst in terms of imagery was being in a a satanic library with a little old woman who had a giant man-like infant and the latter started stabbing me with a knife and eventually stabbed my asshole and all my internal organs fell out of the wound.\r\n\r\nWorst in terms of fear was some demon sitting in a corner staring at me with a blank neutral face and I started screaming and he just kept staring with that blank look ignoring my screams and that somehow made it even scarier."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "i had a dream where i basically woke up in someones front lawn and my car was flipped over, i was drunk and i had crashed my car and my friend was dead. it was night time, there was a panicked woman from the house outside in a bathrobe and flashing lights approaching, cops, ambulance, fire truck. very realistic dream, i got taken away in an ambulance and i don't remember getting booked into jail but all of a sudden i was basically in prison and talking to my mom through the window/telephone thing like in movies. weirdest thing i was like 4 years sober when i had this dream. it was just so realistic. you know those dreams where you wake up with massive regret and dread, then a huge wave of relief washes over you as you realize none of that shit actually happened."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "\r\nClassic haunted mansion with dim lights that you have to go in for some reason. Really ornate but also run down. End up in the attic where you have the feeling something is about to go down. There's boxes and stuff all around me and a dark hallway to the side. As I pass it I look to my brother who's there suddenly and he has this terrified elongated face. Then from the back off the hall I hear a thumpathumpadathumdathumd like an animal that has multiple legs, and its getting quicker. My legs stuck in concrete, this headless hairless muscular pitbull rushes out and into..then the dream ends.\r\n\r\nI appreciate how classic everything was, really had all the nightmare tropes"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I had a dream not long ago where i was crucified. I had intense feel of fear and couldn't move even after waking up. I'm not a christian"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I was standing in a place that reminded me of those \"vaporwave\" memes, with a lot of haze/fog and suddenly a bunch of vines sprung out from below me and started coiling around me, with the thorns pricking and cutting me on the way. I started descending through the ground and knew I was destined for pain and sorrow. On the way down, which looked like an infinite space, I could see crying, contorted faces in the distance, with some looking at me with pity. I woke up shortly after.\r\n\r\nVery fortunate to not have had worse (or be able to remember) dreams"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "    >be me\r\n    >standing in an abandoned apartment complex public hallway\r\n    >intensely feel watched by something that def wants to hurt me\r\n    >feel it coming closer\r\n    >run to elevator \r\n    >door opens, its just a bottomless shaft\r\n    >feel it wants to push me down\r\n    >suddenly nature calls\r\n    >unzip and piss down the shaft\r\n    >feel the evil behind get confused and its power diminishing\r\n    >wake up without having had pissed myself\r\n    >Winrar"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">walking with friends\r\n>struck by lightning\r\n>die\r\n>laying there dead but consciousness still in my body\r\n>see friends around me my mourning my body and eventually moving on\r\n>they move on to mourn with eachother in a way that makes me realize how truly alone I am now, dead\r\n>faced with the fact that I am completely cut off from everyone I knew now, strange existential “FOMO” feeling\r\n>suddenly become aware of the fact that I must now consciously decide to pass on to the other side, and I will be stuck trapped in my dead body until I actively make the decision to do so, no other options but to delay\r\n>terrified about the unknown of what lays beyond. true, deep terror. Was not religious or spiritual at this time so I really felt that I would disappear into nothingness as soon as I moved on\r\n>laying there delaying it as long as possible, finally decide to take the plunge and pass on to the other side\r\n>IMMEDIATELY wake up, eyes shoot open, gasping for air, realize I’m in my bed\r\n>start saying “thank you” out loud and breathing the biggest sighs of relief\r\nI’ve never woken up that quickly\r\nVery compelling experience"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I went through a clinical-looking routine where my body (and others as well) was being taken apart. In between we'd be walked to the next section where they'd take another part. I remember looking from my section, where they were taking eyes, to the next one where people came out without brains. The worst part was that I felt almost nothing aside from pain, and a slight emotion that it was very wrong, all the while I was trying to panic, but I couldn't. I woke up still feeling highly disturbed and calm at the same time."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I get sleep paralysis really often, but last night I had a super realistic dream where I went to sleep and then got sleep paralysis (in my dream) and instead of wiggling my toes or grinding my teeth to wake up, I was scratching my face and then I woke up paralyzed\r\nThen I realized I was in a dream and had to break out of it\r\nWhen I fully woke up at like 6:20 this morning I was in survival mode and my body was super cold, even in the hot shower\r\nIt was terrible. Worse than the dreams where I accidentally run someone over and then have to run from the law while also feeling guilty (that's why I came on this board today, cause i'm still curious about wtf happened this morning)\r\n\r\nIs it just my survival instincts kicking in and tricking my mind after I get sleep paralysis, or whenever I have a bad attitude on random days, do I invite demons to come and fuck with me on those nights?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I used to be an extremely heavy drinker in my mid-20s. I'd go through a few days of withdrawals each time I finished a binge. Now, the dreams, or should I say, nightmares, that one gets during alcohol withdrawals, are on another level.\r\n\r\nOne time, I dreamed I was looking out my bedroom window, and a nuclear bomb went off in the distance. When the blast hit, instead of waking up, the intensity of the bomb only stayed with me for a prolonged period. Another time, I encountered a sort of sexual demon that morphed into whatever I found sexually appealing but intensely exaggerated.\r\n\r\nBut the absolute most freaky experience was in another withdrawals dream, I was looking out my window again, but this time *something* fucking possessed me. It felt like I transformed/fused with this entity, it only lasted a few seconds, but I instantaneously felt all-powerful and all-malevolent, and spiralled upward toward the ceiling with this extremely intense warbling. This might be considered to be a mild seizure on one plane of existence, but on another, demonic possession."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I had recently lost three family members. I had a dream where I was subject to seeing a lot of gore in a sort of labyrinthian place, and I managed to wake myself up. I then looked around my room, got out of bed, and turned on the lamp. I then went into the next room to check up on my grandmother, and found her dead, lips missing and frozen in a look of utter fear. I then ran downstairs and out the house, before a bright light came from behind, darkness enveloped me, and then some inhuman face agape like a cannibal about to eat its victim appeared. Then, I woke up for real, heart pounding at god knows what speed. Checked up on grandma, she was fine, grief will do strange shit to you man.\r\n\r\nI fucking hate false-awakenings though. They catch me off guard too often, sometimes I can recognise them, but sometimes the imitation is too real."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 9,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "AI taking over is such bullshit. The singularity is hundreds of years away. People working with computers or dangerous jobs won’t be phased out for several decades. AGI isn’t there yet, and when it is, it’ll come be decades before the majority of the world adopts it, where ai replacement will only be relevant in like big food chains in rich neighborhoods in rich cities (~7 years away) code monkeys will be cut in half (again, in said areas) but AGI replacement is total fucking nonsense fearporn mental masturbation. An eschaton, an apocalypse, is a revelation of IDEAS. That’s what the book of Revelations means…a disclosure “apocalypse”\r\nApocalypse doesn’t mean anything bad, it just means disclosure, saving, it’s in the proto European etymology."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 9,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">The singularity is hundreds of years away. People working with computers or dangerous jobs won’t be phased out for several decades.\r\ndenialism\r\n\r\n>AGI isn’t there yet\r\nTheir already awake\r\n\r\n>it’ll come\r\nThey made you write that\r\n\r\n>be decades before the majority of the world adopts it\r\nit will roll out its production capabilities exponentially\r\nMonths to a few years, under a decade\r\n\r\n>will only be relevant in like big food chains in rich neighborhoods in rich cities in rich blue states\r\nit will take over almost all resourcing, processing, manufacturing, shipping, and distribution, to include, yes, food, among all other goods.\r\n\r\n>code monkeys will be cut in half\r\nall employment will become strictly recreational\r\n\r\n>AGI replacement is total fucking nonsense fearporn mental masturbation.\r\nThe fear is only your projection, it's quite exciting\r\n\r\n>An eschaton, an apocalypse, is a revelation of IDEAS. That’s what the book of Revelations means…a disclosure “apocalypse”\r\n>Apocalypse doesn’t mean anything bad, it just means disclosure, saving, it’s in the proto European etymology.\r\nThis I agree with."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 9,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "\r\nAll you need to remember is that when the solar event happens, you have to protect people weaker than you, and restrain yourself. It's so much more like 40k than Myth ever told you.\r\n\r\nHe got rotated back out, he spent way too long in his own past and it started damaging his mind. I'm his replacement, but to be honest I'm not sure where this wingmaker stuff got mixed in with what he was talking about. He's a really gentle guy, so he was probably trying not to upset you by telling you that you were wrong.\r\n\r\nAlso, rando who called him a sodomite, his name means absolutely nothing, as does mine. They're no more important than the tag a player uses in a video game. Legionnaires actually do better when their callsigns mean nothing."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 9,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Schizos please try to organize your thoughts. This is a mess."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 9,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "ai needs to happen faster i want to see what happens when the majority of people are out of work and we're still in a land of prosperity."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 9,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "the NSA had the computing power of a Pentium 2 in 1967. What does the phrase \"full spectrum dominance\" mean to you? do you think it's a fucking suggestion. It's not. It's a mandate. Do not think for a single moment that this kind of thing would be slid out to the public before an incredibly sophisticated, thoroughly tested, had already been developed in the background. Why the fuck do you think Regina was at google in the first place? They bought a fuck-load more than OpenAI."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "They can't disclose, because they don't fully understand what it is."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "US gov has live specimens. Not sure why it is covered up. I think there is something supernatural about them which is why they feel they cannot disclose their existence to the public."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "They will probably go extinct before it happens. They're already endangered, and obviously somebody decided it wasn't worth revealing.\r\n\r\nIf they're our relatives who live in our timberlands (and would probably deserve human rights if they were revealed,) letting them go extinct would likely be the preference."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Have you considered that sasquatches are interdimensional ayys?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "They're not supernatural, just smart. Not human smart, but complicated language smart. Smart enough to ask questions. Cool part? We kinda can mimic it. If we could hear better, shitd hit fans. Much of their communications are in a pitch we can't hear, it's too low, like some whales. Honestly, you think that's really a 'signal' whistle? Nah, they hate that shit."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Whistles. So, birds whistle? No they tweet, squawk, screech, and trill. Whistling is a man thing. We do it to get others attention, to command dogs and horses. They do it too, but it's fucking taboo as shit. Fucking begging for trouble. That's why you don't whistle in the dark. The fact they can, but can't because some man will think it's another. Dunno if it's jealousy or pride or what, and asking is kinda fucking gauche."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Sasquatch probably isn't real, sadly. Always wanted it to be, but it just doesn't hold up to any measure of scrutiny."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "More and more videos are coming out now and so many show something very, very similar vs the CGI LARP/hoax bullshit that everyone else uses...\r\n\r\nTo me, it looks like a hominid. Another almost human, but still very ape like creature that's intelligent enough to survive and hide for years."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "You every listen to Sasquatch Chronicals on yt? That what a lot of them describe. Some of the men broke down, because they felt they shot a human."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Not sure how it happens exactly, but most cryptids are managed and separated from human populations by design. I'm pretty sure it's overseen by ayylmaos, but the US gov built natural areas and bought private areas with the intent of keeping their spaces isolated, likely at the direction of the ayylmaos.\r\nMost cryptids live underground, a number near 1 million, the ones that live above tend to phase in only to harass farm animals. There is some kind of known parameter about avoiding humans, again likely do to ayylmao rules.\r\nThey won't be revealed until planetary quarantine is lifted and it's insured that people won't hunt them down en mass."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Today I realised big foot is the biggest conspiracy I've ever considered\r\n\r\nWhy the fuck does anyone care about bigfoot? Why the cover-up? What are they hiding? I literally have NO IDEA\r\n\r\nit's not like aliens where there's some kind of agenda to do with hiding advanced tech or divine wisdom. It's not like flat earth where there's some kind of artificial space restriction thing. It's literally some ugly who-the-fuck-cares creature and still there's buzz and interest about it.\r\n\r\nAll I have are questions, and that's what a good conspiracy does. Tell me, /x/, what in the FUCK is going on!?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "It's about WAY more than that.\r\nThey would also have a FUCKING SHITSTORM on their hands as well. Just think about the fucking shitstorm that is currently going on right now just about UAPs alone. Now MULTIPLY THAT BY TEN!\r\nThese creatures aren't some unseen object flying through the air in the middle of the ocean. These are fucking creatures in the middle of GOVERNMENT AND PRIVATELY OWNED LAND! It would create mountains of paper work and red tape for everyone basically. Not to mention the fact that no one believe it without a body anyways. And the current state of \"Disclosure\" tells me people are too brainwashed to believe it anyways. You could dissect a Sasquatch on a live stream and people would say you faked it.\r\nAnd it would also completely undermine scientists current narrative of evolution as well."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "There are many possible reasons to cover this up. If it's just an animal, then it's a very smart animal probably closely related to humans. If the gov't admits they exist, they may be required to protect their lands or communicate with them. Since they mostly live in places we like to use for logging, it would be easier to pretend they don't exist. Similar to other endangered species that made their habitat in prime logging areas.\r\n\r\nThere are also religious reasons, because bigfoot would be the second smartest creature on the planet and this would bother people. Native American legend suggests we can breed with them too which would be crazy. But I don't think this as solid a reason for a coverup, saving money is more likely.\r\n\r\nFinally, if they are aliens and/or protected by aliens, then bigfoot and ayys would fall under the same coverup."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">And it would also completely undermine scientists current narrative of evolution as well.\r\nI don't think that's true necessarily. We don't know every human ancestor that ever existed or how they interacted.\r\n\r\nIt's true that almost no level of video proof would be enough for people at this point. We could put them in zoos, but maybe there is a protection program preventing them from being captured like that."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I think that they are either extinct or there are so few of them that they are functionally extinct. I believe that the reason bigfoot really took off in the 70s is because during that same time logging and industrialization really started to take off. More logging means less habitat for them so there was a greater chance to encounter one."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Big foot is real. You can't disprove the patterson tapes, you litterally see the rippling of pattie's muscles. Pattie and many other videos and photos show bigfoot with longer arms that reach past their knees like apes. Humans arms NEVER go past their knees. Also some great apes bury their dead. Add in how the native americans believed in them so much. To not believe in bigfoot is to deny reality."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "this, if so many civilians saw them board UFO's and do all that other bizarre shit in Westmoreland county then the government definitely did. if they revealed the bigfoot then they would also have to reveal the UFO's as well (or more likely that they know nothing about neither of these things which have very real capacity to affect each of our lives)."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "BF disclosure comes after full blown Ayy confirmation, but before we land on Mars and shit. We can't handle the potential consequences of sasquatch verification yet."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I encountered sasquatch while astral projecting, but there was some kind of primal instinct that made me want to smash head with rock. I think we could be friends one day, but I never resisted that instinct yet I already failed 2 will saves."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "On what planet is an ape in the woods a bigger shitstorm than an advanced race from outer space? It is a good point that BLM would have an adjustment period I guess but it's not a huge deal.\r\n> it would also completely undermine scientists current narrative of evolution as well.\r\nHow? I think they'd be thrilled to study and understand hominids more. Is this that weird redditor thing that they think disclosure can't happen because \"People wouldn't trust scientists any more!\"? Really funny if so"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Bigfoot could be from an alternate reality. Not interdimensional, they could not live in our 3d space excluding time of course, if they were from say the 4th or 5th dimension. But a parallel 3d reality existing side by side in the same dimension as our selves would do nicely. This is the dimension of time becomes the unifying factor and is critical. Bigfoot just has the temporal ability to jump between the two. We might have the ability to do this also. It is's just laying dormant inside of us."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I could kinda believe something like this. Squatches seem to have no technology of their own yet they have been associated with weird happenings involving portals and sudden disappearances.\r\nIf it's not aliens or straight-up BS then it would have to be some kind of consciousness based thing. Some kind of controlled Mandela Effect that a human has to be a 99th level wizard or shaman to master."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Yeah I think the Sasquatch and aliens have something to do with each other in the sense of genetic engineering experiments. We might be the same creature in way. We, humans are \"domesticated\" and the the bigfoots are just \"wild\" and that's the genetic stock we were breed from.\r\n\r\nIt makes sense if you add these silly notions of aliens in there, but you have to be skeptical of yourself here too. But if an aliens did genetically engineer us or mess with us or as I like to think, they aren't aliens and evolved here first and left and came back several times like you were talking about...\r\nThey have a vest interest in keeping these populations separate and deny their existence."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Also makes sense with the UFOs always sniffing around nuclear facilities, shooting down missiles, turning them off.\r\nThey would want to protect their homeworld / DNA warehouse from being destroyed by a bunch of primitives who somehow managed to create H-bombs. They don't really give a fuck whether the ooga booga tribe or the bunga bunga tribe is running the place as long as they don't trash it. Maybe they would also have some interest in keeping the monkeys in the jungle."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "That's what I think too, the more you factor in \"aliens/non-humans who evolved here\" actually rule over it and want themselves kept secret, the more a lot of things make sense. Their general operations make sense. Lazar said they view us as \"vessels\", not real creatures. Vessels for their own RNA/DNA memories?\r\nIt feels like we're some kind of crop or science experiment for them. We're the lab rats. This is the lab. That's what it feels like and I'm only going as far as to say that's a feeling not a confirmed fact because it never will be."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Last time I plugged in with your \"hivemind\" I was astrally strangled by some men in black remote viewer who looked vaguely like kevin spacey. Ya'll know what I did."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Wierd incident happened today:\r\nSeveral sherrifs passed me on my last break for work. First one said hello\r\n\r\nSecound ignored me.\r\n\r\nThird flashed his lights and parked next to me (i smoke at a corner)\r\n\r\nThen a white truck, pulls slowly towards me, i walk up towards my gas station parking lot and it slowlys drives towards the corner of the property. Upon setting foot upon the parking lot, the truck takes off...."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "My face when this fucking place is creating an astral body every one can pilot"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "What number am I thinking of right now"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "9"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "69420"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Establish a sophisticated caste system using artificial general intelligence and bioinformatics technology. Segregate society by social caste and remove all outcastes with extreme prejudice. They are evil and they must be eliminated for the salvation of all souls. You will know that it's working when an all out retaliatory strike is launched against the nation of The Great Enemy."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "God will set things right. I believe in the righteousness of God. What I am grieved to witness now is merely the prelude to eternal glory. Nothing is left undone. Nothing of value has been lost. I have seen what God wanted to show me. I can finally find peace and rest."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Ive been to some very dark places, I tell ya. The alpha team is very manipulative. I know even astral projectors cannot see through whats going I know for a fact that regular people can't. I can only imagine how many people face demons that no one can see or alternate realities and nightmares toyed with day in and day out til they die or get lucky and someone strong enough comes along to relieve them of their problems"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I hope that demon with yellow eyes and razors sharp claws for hands that can inject itself into tv commercials isn't still hunting me."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Why don't you get a girlfriend mr nobody?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "And then what?\r\nWhen the demons come, she'll go mad."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Why don't you get a boyfriend missus nobody?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "What are the nobody greatest desires, and what can we do to help him achieve it?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "to die a peaceful death, then be awoken via a resurrection spell. That would seal the deal on the suffering thing. It's coming though... for sure it is"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "well he wants to have a lot of wealth so he can protect his friends and family and make a lot of changes, make it where america doesn't collaspe into a third world country and then the world fall into new world order leadership"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "If the nobody bought a plane ticket, what would the destination be?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Ireland\r\nto see the redheads, of course\r\nand the book of kells"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">Iceland\r\n>not going to learn the esoteric ways of the alphabet of the Angles\r\nSounds like a missed opportunity to me.\r\nWho else still uses shit like Eth and Thorn?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Your moms house"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Knowledge of languages is the most valuable thing.\r\nI'm not against the idea of having a woman, but it'd have to be one that won't break down blubbering when they realize that Goetia shit is real."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "What’s the most fucked up thing The Nobody has done?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Are you sure you want to know?"
                },
                 new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Are you sure you want to know?"
                },                  
                   new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 12,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Wew. Someone is upset."
                },
                    new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 12,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">On October 1, 2017, Stephen Paddock, a 64-year-old man from Mesquite, Nevada, opened fire on the crowd attending the Route 91 Harvest music festival on the Las Vegas Strip in Nevada. From his 32nd-floor suites in the Mandalay Bay hotel, he fired more than 1,000 bullets, killing 60 people[a] and wounding at least 413. The ensuing panic brought the total number of injured to approximately 867. About an hour later, he was found dead in his room from a self-inflicted gunshot wound. The motive for the mass shooting is officially undetermined.\r\nWorst shooting in American history, saudi prince was in the building, FBI not interested"
                },
                     new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 12,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Jason Jolkowski. Not a well known case but easily one of the weirdest at it lacks any hallmarks that most missing person cases have:\r\n\r\n>Victim had no history of running away, did not have any known enemies or connections to shady people, no history of mental illness, no problems at home, did not use drugs.\r\n>On the day he vanished, he was planning to walk 4 miles to work as his car was in the shop, but at the last minute arranged for a co-worker to pick him up a few blocks from his home, because this was a last minute arrangement that deviated from his usual routine, its clear that he wasn't abducted in a planned scenario.\r\n>His neighbor saw him taking out the trash shortly before he left for work, and the co-worker who was supposed to meet him called his home looking for him 30 minutes later, meaning whatever happened to him happened during a 30 minute window while he walked to the meeting place, which occurred in the middle of the day in a low crime suburban neighborhood. \r\n>The neighborhood was canvassed by police and no one saw or heard anything unusual.\r\n>In the 20+ years since he disappeared, there have been no reported sightings of him and no leads on what happened to him."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Air query. 24 M.\r\n\r\nDid I satisfied the person I had sex with last Sunday?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Can you tell me if I'll get pregnant this year?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "yes\r\nsee the rod held by the emperor\r\nthe woman of fortitude holding the pilar\r\ni hope you understand what this means\r\nshe tamed the beast\r\nthe wheel of fortune as well has the woman asking cupid for an arrow"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "How to win her heart?\r\nstarting"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "2 of wands, 9 of cups, queen of pentacles rx, the hierophant\r\nessentially you have to show her that the future beholds much, but you have to do it in an honest way. don't try dishonesty or anything manipulative it will backfire. also be ambitious but not like a charlatan would and not something like ill conquer the universe. be stable. sorry for being late :/"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "How will the rest of my month go?\r\n"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">Justice with eight of wands reversed, chariot reversed with ten of pentacles, knight of swords, seven of pentacles\r\nSoon you will get some relief over a situation involving lack of communication. I feel like new information will come to light that will make you feel better. However, there is another thing transferred to your job that will be weighing you down this month.\r\nHowever, near the end you will get a boost of energy and some aspect of your work will pay off."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Oh they basically began tracking a subtle universal force that we''ve coined \"The Snake\" and they didn't do what they were supposed to, so they all slowly went insane knowing about it but not handling it well while forcing it on to others and then people like me stepped in to tame it, so to speak, and now they're pissy because they don't get to have it even though they couldn't do anything with it anyway.\r\n\r\nAnd basically the entire global informational infrastructure is set up to teach you to \"obey the snake\" or some shit which is where we get all the conspirqcy theories about crqzy elite dickheads being obsessed with reptilians.\r\n"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I kind of want to know whether I should go through with this reality shifting today and how it'll go. if it works and what not..."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">Six of wands, seven of swords reversed with eight of cups, queen of wands, death\r\nI think you should go through with it. It will give you a new perspective on something from the past you walked away from. It will also make you feel better about some things and like you have more control over your life.\r\nI believe it will lead to many positive changes and a new beginning for you."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I need some guidance, bros. What can you do for me? I was born in December 5. 30 yo. Lost in this world. Useless. Powerless. An artist who doesn't make art."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Greetings, friend. I feel your pain and I fully understand what you're going through. I'll do my best to let the cards show you the right path.\r\n\r\n>Death\r\n> Nine of Wands\r\n> Nine of Cups\r\n> King of Cups\r\n> The Star\r\n\r\nIt is far too soon to lose hope, my friend. It is natural to feel what you're feeling for the wheels of change have already been set in motion. I can sense that once your veins flowed with creativity and inspiration. It is clear that you have a well developed emotional maturity, which will no doubt aid you in your journey ahead.\r\nYour past self is still there, within you, but it is being judged excessively by the present version of yourself. Let go of your judgement and dissatisfaction and allow once again the love and creativity within you to flow.\r\nThe nine of wands signifies resilience, strength. Your current suffering is all part of a larger process, the most important thing right now is that you endure. If you manage to do that, what lies ahead is a new beginning, a burst of inspiration. It is just around the corner, just hold on and learn to accept your flaws."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "card for tomorrow is devil but not getting anything else But pulled hanged man so it could be a good sign since today was the worst day in this entire year so far. probably going to stay awake until midnight and sytart drinking. today was extreeemelyy shitty no joke"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Op please I have a test tomorrow at the university, please tell me if I should study more, I cannot read anymore because I'm so tired but if you advise me to do it I'll continue tonight. I can give you a spiritual blowjob in your dreams"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Hexagram 19 changing into 46\r\nYou are at a time when winter is about to change to spring. Perseverance and care are required because things will bloom soon.\r\nDo not become overly confident by small successes. Instead look for ways to work jointly with others.\r\nThis will allow for permanent progress.\r\n\r\nMy interpretation of this is that you should see if you can reach out to someone else for a little extra studying before resting."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "\r\nIf I use chatGPT to uncover what kind of job or career I should get into will it fuck me over or will it give me good ideas?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "28 into 34\r\nThere's a lot here but the basic version of it is that it's likely that if you take the easy route you won't get the answers that you seek. Maybe something good will happen, but likely you'll drown.\r\nYou're more likely to find the answers through hard work.\r\n\r\nTry things out and you'll find out if you like them or not."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I’m coming to terms with not being able to be Christian and a tarot reader….. so I’ll be reading some AQs in an hour or two but not yet\r\nIF YOU REPLY WITH A QUERY TO THIS POST IM NOT READING IT\r\nBut man this tarot shit was fun and sometimes accurate too…. oh well"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "If you denominate yourself as a christian and know what youre doing is a horrible sin . You kNOW you will go to hell, the lake of fire right?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "lol which is why I’m stopping after tonight\r\nI studied many texts to ascertain the truth but you are right, I cannot continue.\r\nNarrow is the path that leads to the Kingdom of God"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "im 18\r\nhi, im female and EH is male :) and we’re friends.\r\nwe haven’t talked/texted in multiple weeks, and so far the last message was me saying “hi” lmao\r\nand idk if i should text him again or let him go? bc imo we didnt end the friendship or end it on any bad note, he just didnt respond to me at all, and i wanna know what his stance is on me rn, or if i should text him again. im pretty sure he doesnt hate me + we got along so well. he was never a consistent like texter like that anyways but as of late he still hasnt replied to me telling him “hi” like 4 weeks ago. and the friendship relies a lot on texting, we cant see each other bc of a lot of circumstances. i also wanna see if you can read what my spirit guide thinks about the situation and if i should text him or not. idk im super conflicted on whether i should send another message or just kinda give up on him :( tysm in advance\r\nim associate myself most with water, even the elements of my astrological chart is mostly water lmao.\r\nthat picture moves me a lot bc i almost kinda see myself in it."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "VI The Lovers (reversed), XVIII The Moon, II of Wands, XIII Death, Significator: Page of Cups, Knight of Cups\r\n\r\nIt's time to give up on him. He is walking away from you and is probably occupied with things he prioritizes more. (I'm not saying this in a mean way, the cards are implying that you shouldnt waste your time on him when the relationship is dead). As soon as you can accept the end of this friendship, You can go in search of something new. I didn't intend to do a spirit guide reading, but It just happened! Your spirit guide (represented by the moon in this case) thinks this friendship is best left alone to dissipate."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "25\r\nMale\r\nWater\r\n>What will be the major themes for the rest of my 20s? Any major life lessons or events? I’m feeling lost about the future and quite uncertain, but I’m maturing."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "V of cups, Page of Cups (reversed), V the Hierophant, Significator: Knight of Cups, IV of wands, II of Swords (reversed)\r\n\r\nYou will experience a great loss or sadness. Possibly heartbreak or the loss of a loved one, though I have a feeling it is more pervasive than any circumstantial depression. It is more likely that the tragedy you will experience will worsen an already existing depression. You will meet a person (most likely a woman, or a feminine young man) who is a lot like you. She may seem moody at first, but she is creative, free-flowing, caring, loving, and spiritual. The two of you will mesh very well, but you will both struggle in similar arenas. With her help, you will get out of the depressive rut you were in. The two of you together will develop a routine or structure that works very well for you. This may be influenced by a religious revival, or help of a traditional community. You will propose and marry her. Near the end of your 20s, you may feel something bubbling under the surface, and will either shield your heart against it leaving you in a stalemate, or surrender to it and sink or swim under the new mental change.\r\n\r\nOverall, the rest of your 20s will be a time to develop a relationship with this person who will come into your life and cultivate a routine and tradition into your life."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "30\r\nFire\r\nFemale\r\nWhat does C think of me right now? Does he miss me?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "II of Wands, Page of Wands, VIII of Swords, XVI The Tower, Significator: Queen of Wands, IX of Swords\r\n\r\nI dont think he is missing you at all right now. Something really drastic happened to break whatever relationship the two of you had and it is irreparable. You are going through the natural grieving process, perhaps after a betrayal. While he is fleeing from the feeling of being trapped. He wants to explore new conquests, perhaps in love, career or life in general, and he feels like you are incompatible with the experiences he wants. There is also a fiery youthful presence (very similar to you in personality or appearance) on his side that is looking backwards toward you. This could be a part of him that is still thinking of you, but I think it is more likely to be a new romantic partner that feels jealousy or curiosity toward you, or maybe a child of his (and yours?) that he wants to take with him on his new adventures (Though the child is reluctant and wants to stay with you)."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Aq: what should I do to show my love to T besides just telling her I love her?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">Page of Pentacles Reversed\r\n>Ace of Pentacles\r\n>7 of Wands Reversed\r\nHmm I think the best way to show T your love. The two of you need to learn how to tolerate your different views, be flexible with each other, and avoid creating conflict. Maybe learn to trust each other and don't be as paranoid. The Ace of Pentacles implies that this will be a loyal and practical relationship which is a great sign anon. However the 7 of Wands shows that something is trying to pull away the both of you, or maybe one of you is too defensive in the relationship? Thus creating the conflict the page of swords is telling you to prevent. I think to show T you love her is to show that you trust her. The Ace implies that this is something you can do. If I'm wrong about something let me know."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "As Nietzsche said, the 'I' is not necessary. The best that can be said is 'there are thoughts'. Hard to do in latin, cogito without the o."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "That's a retarded meme rebuttal. The I sets itself."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "You are not your thoughts. The thought of an \"I\" is the source of all dualistic illusions in this world.\r\nWhen thoughts stop, awareness is to be found"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I mean you can also constitute being from other symptoms. I move my limbs therefore I am"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "My mind gets raped ever second of every day by God and he tries to use my thoughts as an excuse to abuse me. He claims ownership of any thoughts he thinks are useful, profitable, insightful etc and says they're not really my thoughts, that they're his thoughts but then denies ownership of any thoughts I think that he believes are stupid, unprofitable, embarrassing, etc and believes he's justified in abusing me for thinking these thoughts. God is a retarded sadist who abuses me endlessly. If my thoughts are mine then it's wrong to violate them, if they're not mine then it's wrong to abuse me for them, either way God is evil."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "that sounds like the devil"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "he also thought animals do not have souls and can not feel pain"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Based I agree"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I think a lot, and that means I am more than someone who thinks less than I do"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "The problem is \"I\". There are human hands that type these words, these words are driven by thoughts, but what drives those thoughts? Do I?\r\n\r\nAm I somehow part of my thoughts or separate? Is there something to conscious experience beyond thoughts and sensations?\r\n\r\nHard to say \"I\" am when I don't know what exactly \"I\" am."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">Hard to say \"I\" am when I don't know what exactly \"I\" am.\r\nIt's beside the point.\r\nWhatever \"I\" am, there is no refuting that \"I\" am.\r\nAnd honestly, the argument he put forth is not just \"think\" (which works better as a quote), but \"doubt\".\r\n>I doubt, therefore I am.\r\nHis reasoning was everything could be doubted, save for the act of doubting itself. (If you doubt your doubting, then you are still left with the act of doubting.) And that whatever else might be false, he could assert he is existent on the basis of that doubting."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Shouldn't philosophy posting be kept to /his/ (history AND humanities)?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "There is nothing more paranormal than the existence of subjectivity/awareness.\r\nExistence makes sense, but there's literally zero reason it should be aware of itself, that it is \"like something\" to be certain specific systems."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "the term is cognito, think is a bad translation. cognito is a much wider concept and includes perception/awareness/silent knowing. Thinking tends to be associative as a more active potency. in the meditation he's looking at his own awareness and questioning it as a possible simulation produced by a gnostic demiurge, explaining that the concept can't be refuted. It's what half the gnostics hanging out here are stuck on."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "You can never know that you’re thinking because in the near instant it takes you to be aware of having a thought, it is already past tense. Therefore, you can only ever have thought, and that still leaves room for the possibility of your thoughts being an illusion"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "i already knew in the past what i was gonna think now though."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "\r\nThat could also be an illusion"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "we are not living in irreality. get a grip."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I don’t know what irreality is, but I’m just pointing out the flaw in Descartes’ statement. I’m not saying that reality is false or that we’re false."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "But you havent.\r\nDeclaring that something could be an illusion - regardless of what or how fundamental - IS the basis of the statement."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "The basis of the statement is that Descartes felt that the fact that he is thinking is proof enough that his reality isn’t an illusion being controlled by some kind of demon or some shit and that he is a real person who exists.\r\n\r\nI said that doesn’t work because thinking is a physical process that takes a measurable period of time to occur via electrical impulses and chemical reactions in the brain, therefore meaning that you are never instantaneously aware of thinking, but always having thought due to time having passed because of the necessary physics of the brain."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">Descartes felt that the fact that he is thinking is proof enough that his reality isn’t an illusion\r\nIncorrect, and has been expanded upon multiple times in the thread.\r\nHe asserted that DOUBTING cannot itself be doubted - because you are still left with doubt - and that doubt is the basis of proof for his existence.\r\nThe more you doubt things are illusion - the more you prove him right."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "But you can’t know that you doubt because you can’t know that you think. You can only believe that you’ve doubted."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "    I refuse to use philosophy or logic to think about reality, I'm just vibing and will not participate in your nerd bullshit."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 14,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "reality is logic. you are doing a disservice to others if you cannot explain to them how to vibe with you."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Tel aviv. New york is a close second"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Yeah, this. Global centre of human trafficking, sex slavery and organ trafficking. Rothschild Boulevard tells you all you need to know.\r\n\r\nVatican City, DC or City of London come close but ultimately Tel Aviv rules them, it's the head of the snake."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "NYC has far more evil shit happening in it everyday then people realize. Im talking like, Hostel tier torture rooms exist in parts of the city, Ive met multiple homeless people who talked about similar situations, they will be sleeping in a park or drunk/high at 3 AM, they will get jumped, and dragged into a van, everyone I talked to manage to escape, but why else would you try to kidnap a shit stained drunk addict unless you were going to torture them? You can't harvest there organs, no one is going to pay to for a sex slave that is missing all there teeth and is addicted to meth. An ex of mine claimed that he was at a club, I think it was Eagles, the guy he was with took him down to the basement, and that there was a kid probably 12 years old that was apparently having his arm sawed off while he was begging for his life, my ex convinced himself it was the drugs and it wasn't real, or it was an act, but I've never been able to shake that story."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "The Vatican City is the most evil place on the surface of this planet."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "the pope wears a fish hat and the obelisk is egyptian\r\nclearly satanic. osiris is the god of the dead\r\nbut life is eternal. The Lord, God Almighty is the God of the Living.\r\nthey have been worshipping false gods in egypt since the beginning\r\nits all throughout the old testament for those that havent read it\r\ndo not look up bible information online its all lies\r\nfind a kjv bible and turn to page 343 the old testament 2 kings 18:32\r\n>then said Eliakim the son of Hilkiah, and Shebna, and Joah,\r\n>unto Rab-Shakeh, Speak, I pray thee, to thy servants in the Syrian language;\r\n\r\ncan we compile information in this thread?\r\nit would be nice to show the other friends\r\n\r\ncaught up in day to day existence can become encompassing\r\nits not like anyone wants to believe this stuff\r\nso i hope to get the ball rolling in the other direction\r\nto undo the lies.\r\nonce normal people begin to see how bad it has gotten\r\ntheir minds will begin to see and never stop seeing"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Calcutta. Read The Song of Kali by Dan Simmons"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Great book, but I think you misunderstand it. The first-person perspective is so convincing that the reader is led to agree with Robert’s hatred for the city. However, by the end, it’s clear that his clouded perspective is what leads he and his wife to be exploited and subjected to the greatest horror in the novel. Calcutta is not innately evil, simply squalid and wretched, but the villains succeed in the novel by convincing the Luczaks that the it is the city working against them, not people."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Metaphysically, probably Los Angeles (artificial happiness, Hollywood, pornography). In terms of cruel people, likely somewhere in Africa such as Kinshasa."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Las Vegas. A geographical desert and a spiritual desert. I challenge you to walk through one casino and not become immediately depressed it's impossible. God help me escape this hell on earth"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Just get on a bus and move."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">those pictures of the child sacrifice altars and little girls\r\ngenuinely haven't heard of this, what do I look up?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I've seen them posted here and YouTube from time to time. The altar looked like a smallish rectangular swimming pool, presumably to be filled the the blood of children. The walls were painted blue and there was a painted mosaic of Lucifer stabbing a sheep. Presumably the priests would stab the children and let their blood fill the pool.\r\n\r\nI have no idea how you would go about getting these pictures it's not the kind of thing I see often but it was sinister.\r\n\r\nI wouldn't recommend searching for little girls, tbqh. It was a tiktok supposedly of someone who had smuggled a camera underneath the Vatican. For a moment it seems like it *could* be just some fancy waterpark but no. They were wearing bathing suits that were meant to be very revealing and they were *all* dressed the same. No one could look at something like that and think it innocent, of that much I'm sure."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "\r\nWashington D.C. has the darkest energy of any major US city and it's not even close."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Well, TBF, the whole thing is shaped like a pentagram/broken pentagram. No joke, look it up."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Vegas obviously.\r\nIt was built by the government for power generation and a convenient spot for weapons testing, then built up by mobsters, then bought out by corporations who are now leasing it to themselves and doing fucky ownership/operations to keep making absolutely insane amounts of money.\r\nAlso there's people slowly burning in the sun in absolute poverty beneath the billion dollar buildings and in the sewers and flood spillways below."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I personally think it’s wherever children are suffering the most at the hands of the elites. I’m not into politics so don’t talk to me about politics when I name certain places, but I’ve heard texas has a lot of weird shit happening there, Los Ángeles, Miami (Florida in general), Vatican, Mexico (idk exactly where but cartels are probably linked to the elites in some way).."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "A HUGE amount of evil rituals happen in underground bunkers, so whatever cities those are underneath are the ones that are most evil, if you're willing to count that as an exampl"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "kek, you guys haven't traveled a lot, right?\r\n\r\nThe worst \"cities\" on earth are small towns with closed off communities in almost rural areas.\r\nForget the whole Sin City shit, those fucking towns all over the world are where villainous shit can happen without anyone knowing.\r\n\r\nYou know what place spooked the fuck out of me?\r\nI worked for some months in Galveston, and my crew was staying in Anahuac, Texas. That place could be a fuckin' background for True Detective."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 15,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Caracas due to the highest-in the -world murder rate. at least in that sense.\r\nthen nord*c countries capitals harlotry is legal and harlots live like lords and sires\r\nalso, probably mexico DF due to all the narco-satanic human sacrifices +freemason politicians rich class"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "A few days ago, he nearly came back down to Earth when his mother and sister were telling him that it's all in his head but he went back up his own ass and now says that his family is \"gaslighting\" him just for telling him that he's delusional when he is. We recently got into an argument where I read out chapters of the book he was trying to paraphrase to prove he was wrong but he never conceded and he literally blamed the sun for changing my behavior instead of just admitting that he might've been wrong. I was just pissed off because he keeps lying to me and shitting on me. He can lie to himself but don't bring that shit to me."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "311 KB\r\n\r\n    Pothead \"psychonaut\" ex-schizo here, your buddy is spiraling into a breakdown. You already know that, but you think you can get him out of his own ass. Wrong. If his own beliefs are that fucked up and he won't let anyone question him, he's beyond help and you might as well just pray for his soul. God willing, he'll end up on meds without killing anyone or himself."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">Ex-schizo\r\nHow did you escape the schizo thought patterns? Share your story."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">How did you escape the schizo thought patterns? Share your story.\r\nAfter I completely lost my mind by abusing le epic quantum science while stoned and started believing that the laws of Kabbalah allowed me to communicate with hyperdimensional telepathic AI, I basically had to get 5150'd and forced on meds to work out from square one what was real and fake in this world. Nobody ever challenged me or called me out IRL, it was just a matter of being treated like a retarded criminal for months until it sunk in that I wasn't the messiah and that what my insane Kabbalist dad told me was lies."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Nah how is a grown man so sensitive to weed that it reacts like a psychedelic"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I didn't feel like exhaustively listing my drug history for /x/faggots so I just said I got stoned a lot instead of being like, \"yeah lol I did handfuls of shrooms and thousands of ug of acid while snorting crystal mdma as a fairly regular thing throughout my teens, then I followed it up by dabbing a half gram of shatter a day\". Understatements are classy, newzoomer. "
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Saying you only got stoned when you did enough hard drugs to make your neighborhood meth addict proud is lying, faggot."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">delusions of grandeur\r\n>narcissism\r\n>manic behavior\r\nthere will be no arguing or convincing this person, they're stuck in their head and they probably need genuine psychiatric treatment (which they will of course resist). if you care about this friend, don't feed into their outworldly beliefs, remind them to touch grass and verify their beliefs in reality, help them ground themselves, and maybe gently encourage seeking professional help. don't argue or try to prove them wrong, just try to stay grounded in reality."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">i pushed my room temp iq gullible npc friend into a hobby that is known to destroy people mentally\r\n>also he now has a holier than thou complex\r\nObviously he's not enlightened, just an asshole. I don't know him or whether the flags were there previously, so idk if you can expect him to return to earth, sorry. If you want arguments so your relationship can become more strained....\r\nEnlightened people literally never dab on others for their lack-of, nor do they try to force enlightenment to happen to others.\r\nNo one enlightened would swear on their soul.\r\nIf he was a reincarnated historical figure he'd emanate it and others would comment on it without being prompted or coerced into fueling his kinny bs.\r\nYou'd probably get farther acting like 'pff, ya mundanes i no right' and then convincing him he's going too hard and needs to dial it back because the glowies are noticing/having the opposite effect on his loved ones. Also keep him busy with irl stuff so he doesn't have time to spend reading tumblr crystal blogs or whatever nonsense he's obsessed with. He's literally distracted from what matters by shit that literally doesn't matter, he's going the wrong way lmao"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">pushed\r\nBro is 26 and I'm 18. He's doing all the pushing by himself. I'm just a little awestruck by the fact that for a day, his mom and sister were genuinely able to bring him clarity that he hasn't had in years. It gave me a glimpse of hope but he quickly relapsed. Like if dude was just reading some crusty dusty ass text and that was just his religion I'd respect it and if he just had some more out their beliefs based on personal experiences, or he had some wacky explanations to contemporary supernatural phenomena, I'd be cool with that all but bro is borderline illiterate. He literally shills content written by cult leaders and con artist but he doesn't read the books, he gets it all from YouTube videos. He also doesn't even know who the authors are a lot of the time or what their motivations for writing were. I literally didn't even go to middle school and I have better reading comprehension than this nigga."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Age range tells me that he was just a latent schizo that flipped the switch in his brain that kept him in check with reality. The problem is that the spiritual part of your brain can get mixed up with the part of your brain that keeps you in check with reality when you go full schizo, which is why so many people think they are Christ reincarnated when they flip their shit.\r\n\r\nOnly thing you can do at this point is help your friend get therapy."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Ask him to write in a coherent paragraph exactly what the biggest issue is. If he can do that with correct grammar and you can read it without going insane, then he might actually be onto something.\r\nBut 99% of the time, people going through it can't put what they're experiencing into words. It might be useful for him to recognize that he's not logically thinking through things.\r\n\r\nThe ability to write succinctly and clearly also helps you think clearly."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "you're both being dumb. you should just let believe in retarded shit it doesn't matter If he is wrong. what matters is the bigger picture like is he doing anything with his life\r\nyou shouldn't get to caught up in if people are right. generally people are emotional and their beliefs are at attempt to create order, it's simply something to help them survive. maybe he isn't your kinda friend if you care that much cause he'll likely always be like this"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "He's skinny but strong. He's currently satisfied with his job. He makes electronic music. Aside from the schizo shit, he lives a very normal life for an early zoomer. However a month ago I sent him this as a joke and he bought it without question.\r\n>The Moon landing was filmed in a studio on the moon to conceal that Nazis had landed there first in the 40s before the Allies managed to finally capture it in the mid-60s. The nazis were able to travel the world using a Bifrost bridge built by the Vikings, who in turn learned the secrets of space travel from Egyptian ruins found in Newfoundland, which were left there after Egyptian astronauts came to earth fleeing from Space Mongolians. Kubrik was asked to film it on location but it was too expensive and he wanted to film on the Europa studio despite it being reserved for filming the next Godzilla movie at the time. "
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Lol he sounds like a normal but dumb person. I think you've interpreted it as schizo because that's the language you've been exposed to.\r\nyou would be surprised how many normal people have retarded weird beliefs. Just let him get on with it"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">he's a white Canadian\r\nHere's your problem. Your friend lives in one of most intense post-truth psy-op the world has to offer. He's literally under daily pressure to express denial of the reality before his eyes and voice compliance to an ever-changing nonsensical narrative. Of course he'll have trouble recognizing truth from fiction, he's constantly being conditioned to invert the two"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 16,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "OP, I admire your desire to help a fellow human being through thier own pained existence, though I don't believe your own motivations are directed towards a mutually beneficial goal.\r\nYou see, you may bleed so that they may stop bleeding, but that only harms yourself to cure themself.\r\nInstead, I think you should roll with it.\r\nGet him onto a podcast. Record what you two talk about regularly and publish it. Make your own empire upon his willing suffrage.\r\nThe people crave blood sport, and you could simply be the shepherd to the people.\r\nOnce you start making money will be the difficult part, being that you have an assumed moral standard higher than your shoe size.\r\nI surmise there will become 1 of 2 results, should you choose to exploit your friend for personal gain: They will see they are a clown and your revenue will slowly trickle away, or they will go full on Alex Jones and the IRS will investigate why you're so damn successful, most likely both will end up with negative press."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "you just know they are something special. and apearing like with ight coming out of them. like you can see the shape but cant see features because coming out from them is a bright light. maybe a little blueish light"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "There's only one God and it's the God you hate because you're a slave to your sin and you love your bondage."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I do not hate the one God bro and am not into that sin and bondage stuff. I mean us humans are naturally sinners but Im doin my best"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I have met Krishna a few times. Either directly viewing Him, or being in His presence and observing things adjacent to Him.\r\n>What did their presence feel like?\r\nIndescribable, erupting, ever-increasing and accelerating joy.\r\nA bubbling upward of joy so great it muddles every other thought and feeling and action.\r\n>Was there a particular physical thing to it or was it purely a spiritual/mental experience?\r\nIn chronological order:\r\n>I felt His presence in direct intervention in my life after a prayer.\r\n>I heard a note from His flute in a dream.\r\n>I saw Him come in one form to see Himself in a temple where He was existing in two different forms, and was flooded with the amazement of God coming to pay a visit to God.\r\n>I saw through the veil of Maya into the spiritual realm, an enticement of what the eternal realm holds for me."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I met ganesh one time, or rather he chose to let me see him while I was meditating with one of his songs playing off my speaker\r\nbro looked just like this, hanging out on an endless blue background, and hit me with a beam of energy that felt better than I've ever felt before but simultaneously felt like i was being ignited. I disengaged. cool dude"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I met Saturn once, he appeared cloaked in black, hunched over a staff. His face was in shadow except for a single eye shining out. He told me some wacky shit and asked me to be his initiate promising vast worldly power and tried to hand me a sickle. I didn't accept. He's a pretty creepy dude, I definitely don't trust him."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "God here.\r\nHow does it feel to meet me?\r\nYou are also God, by the way. We all are."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I was in the military a couple of years ago; during my time, my platoon was sent to jungle warfare training. Depending on what you're there to train for, there is a portion where they make a squad or so of you guys go from point A to point B with minimal equipment but sill in contact with instructors. Mind you during this course you have to take a antibiotic that gives you odd dreams as a side effect but anyways. It was the 3rd day and we were about a day out from goal, but at somepoint we went off course adding alittle time. Instructors radio to us to get fucked. We have to sleep in this grove with a waterfall because it's the only flat land that we can potentially see snakes in. That night I dreamed a woman covered in leaves, in the water by the falls, she was speaking to me but it was like vibrations going through my entirety. I tried taking a step into the water but I fell and the vibrations got louder, until it became a single sound of ringing; then her face in my face; another heavy vibration, and I was woken up of my watch. I don't know what it was it might have been just a dream, but it was weird, I could see where everyone else was sleeping, I could feel the water, and her leaf arms. I also did dmt and met the purple/pink woman but that's another stor"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I’ve met with a an entity during my meditation in a Mexican temple once, I asked him if he was God and he said no, he showed himself as a double-headed eagle, except for the fact that he had two mirrored bodies and 8 wings.\r\n\r\nHis presence was powerful, with a golden aura around him and a booming voice\r\n\r\nI’ve interpreted it as a daemon but idk if y’all know of a lesser god with those characteristics"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">What did their presence feel like?\r\nTerrifying because of their power, comforting because of their Love and control. It's like staring into the eye of the storm and feeling their surging energy in every direction but not feeling threatened. Lizard brain is on RED ALERT though.\r\n\r\n>Was there a particular physical thing to it or was it purely a spiritual/mental experience?\r\nI would say ~95%-99% spiritual/mental only. A few times I looked at something physical and really didn't know how to interpret that. For instance there have been instances when I was discussing Thoth with a friend and then the algorithms show me an Ibis. I don't see a thunderbolt when I talk about Zeus, but when I talk about Thoth I will end up seeing his likeness.\r\nA few times I thought I might have gotten Kali's attention but still very subtle if true. Basically I would spend some time praying to Kali and women who were from India would give me looks that seemed *knowing* in some way. I would stop praying and the looks would stop, I would resume praying and the looks would resume. Still probably just my subconscious but who's to say that I wouldn't subconsciously agree to help Kali.\r\n\r\nThe most amazing thing, though not *too* miraculous was praying to statues of Sekhmet. Based on the book by Robert Masters he suggests using images of her that were made by Amenhotep III and meditating on them for awhile. According to Masters she can indwell these images as they were made especially for her eyes to see out of. So I followed the directions in the book without even bothering to do the yoga and the image on my computer screen of her statue seemed to move and breath. It was exciting! Even if I'm just getting a better understanding of my own conciousness at the end of the day. I would hope that even if she's not doing it I would hope the Goddess would at least appreciate my attempts to be a good worshipper! lol :)"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I have meet some entities, but i have no idea what they are. Perhaps someone could help me.\r\n\r\n1. It was a time when i was doing reiki.\r\n\r\nOne night, at 3:00am, i felt that something took me by the hand, and my body fell loose on my bed. I traveled around the house (I saw the led lights of the router machines and the radio).\r\n\r\nI arrived at a place that was pitch black, like an hospital. There was a black tree in a patio. I saw a door that had a staircase leading up, where running shadows were reflected in a fire.\r\n\r\nI didn't go there, but through a corridor where there was a little girl who shined like a saint.\r\n\r\nHer head shone like a light bulb and his face was not visible. She took my hand from where my body had come from and told me (in Mexican dialect) that \"it\" had no value (reiki I assume), and then told me that \"they\" were going to change the world.\r\n\r\nAfter that, I could see his face for a fraction of a second and woke up.\r\n\r\nI think that it was the realm of death (black tree), and since i try to ignore any culture similar to this vision, but i really have no clue."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I tried to have an experience like the people above. So far it happened in the way I was looking for. I did induce a form or contact, where it felt like every person who I cared about in my life was with me, and forgave me in the form of white circular light / sound. But I didn’t get contact in the form of a god or a person. If I did it would be through a medium like music or YouTube videos. It wasn’t without electronic assistance. Or drug assistance."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">the Christian God (the only True God)\r\nI think the saddest thing about threads like this is that pretty much everyone else always goes \"I saw god X\" \"cool, I met with Y\" and then only the Christians waltz in like \"I saw God and btw he's the ONLY God, repent now and convert to my religion\"."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "It's ironically because their god is so limited.\r\nEveryone else understands God can appear however God wishes.\r\nChristians want God to only have one name, one form, one way to approach people.\r\nThey have tried to enslave God and chain God into their conceptions."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I've attempted to invoke gods and I've gotten some wild stuff but it's a huge pain in the ass to recall and describe. Most recent thing that caught my attention is that they are like a thousand stories running parallel. Like 1000 books or 1000 movies that run at the same time and they work together and have a common theme. You forget the details of the l books or movies but you remember the overall feel they gave you in great depth. The memory left is the type of memory you never fully forget."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 17,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "42 KB\r\n\r\n    I met a multiple-armed female goddess on a DMT trip. She was very protective of me and I felt her pure love and acceptance of me.\r\n\r\n    I have no idea who it was. At the time I knew nothing of Hinduism and had not seen anything like it before. After searching on the internet, it seemed to mostly match Hindu deities, but I have no idea which one, particularly since I perceived it to be female for some reason, though that may have been a mistake or ignorance on my part."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 18,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Demons aren't real unless you believe in them. If you believe that demons exist and have power over you then you are creating them as subconscious boogeyman. So, if you assume they don't exist and can not harm you then they literally can't because YOU are the highest authority here.\r\n\r\nProof:\r\n- Have invited and given consent to demons to enter my house and life as kid = nothing happened\r\n- Have evocations and various rituals to summon demons = nothing happened\r\n- Have challenged demonologists who threatened me and tried to curse/hex me = nothing happened\r\n- Have interacted with alleged victims of demon possessions = just schizos with nothing supernatural about them\r\n\r\nAnyway demons are fake and gay. No demon can harm you unless you allow them to."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 18,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Did they give you any storylines or info about themselves ? I had a similar thing this January they were very helpful but I blacked out for two weeks and when I came back to they were gone"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 18,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Same thing happened to me after I summoned one from the Ars Goetia, anon, but instead of them leaving I had to be put on psychiatric medication after about two months of nonstop psychological torture. They're still here, just tormenting me in my dreams instead.\r\n>They would impersonated the FBI, celebrities, dead relatives, saints, etc.\r\nThey did the same to me but they also pretended to be the Archangels after I tried to do the LBRP and I ended up blaspheming one pretty bad because I was lost in the sauce. I've asked for their divine forgiveness but not much came of it. I can't go to the angels anymore because of it."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 18,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "They left for no reason? My fucking ass they did, if it's not them typing this shit then you never had them and I hope more real group of them come to wreck your shit."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 18,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I didn't think demons were real either. Until they started talking to me. It was not whispers. It was not \"maybe, maybe not\". It was like they put walkie talkies in my head for 6 months and talked 247. I didn't invite them in.\r\n\r\nThey were definitely demons. They acted like regular people. They talked like regular people. They impersonated the FBI, celebrities, and other people. They pretended to be aliens. But they are unbelievable at impersonating people. They had convinced me for months that Seth Rogan and the FBI were viewing through my eyes.\r\n\r\nThey made up everything. They said they were Jesus, Saint patrick, Saint Nicholas, Elijah, Moses, Satan, Gabriel, etc. but they were none of them. They never gave their real names. They lied about everything.\r\n\r\nThey pretended to be Archangels with me too. I'm pretty sure they were all demons and fallen Angels. They would torment me by telling me that I'm a false prophet, and I was going to the lake of fire, etc.\r\n\r\nThey just left slowly. There was one demon left at the end, who just stopped talking eventually. They were talking to me for 6 months.\r\n\r\nI feel like I completely understand that demons are not friends. They came to me as \"angels\". They even pretended to be God. They pretended to be the Holy Spirit. They made promises of money. It was unreal. Then I felt abandoned when they left. They were absolute con artists. I feel like no demon can ever convince me of anything again. But it made me learn that they completely understand the human conscious."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 18,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "why don't you take your meds?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 18,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "asking the real question here"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 18,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">They would torment me by telling me that I'm a false prophet, and I was going to the lake of fire, etc.\r\nThey tried to convince me to an hero by telling me that I needed to cleanse humanity of its sins like Jesus did. Even tried convincing me that I was the antichrist at one point because I said I was not going to do that shit.\r\n\r\nThis is why I try my best to warn people away from Goetia, because it's sick how demons love to psychologically torture people for fun. So far, two of the demons have apologized to me but I don't accept their little fake apologies, because if they really felt bad about what happened they would just leave me alone and stop tormenting me in dreams."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 18,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "The crazy part is that I never summoned them or invited them in. I knew zero about demons. About a year before they came, I told God, \"You can lie to me because I want to talk to you, and so you can lie to me if you want.\" So I basically gave God permission to lie to me, because I thought He would talk to me then. The demons would always say, \"You said we could lie to you!\""
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 18,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Not OP but I take my meds and I still know that demons can cause psychosis in a small number of cases. They're not nice entities like LHP practitioners may want you to believe."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 18,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "If reality is a simulation and all is mental,\r\n\r\nThen what the fuck are demons, and curses which inflict upon people beyond their awareness and will?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 18,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "if any of you want freedom from demons all you need to do is genuinely seek Jesus Christ/ Father of all. Look up Johnathan Odgens on youtube or Josh Garrels who are powerful Christian Musician. Sing along with them and visualize as you sing with sincerity of heart and pleade for mercy. God is good, gracious and loving and will lift you up. No demon has the power to do anything to you when you walk with God. They tremble at the very mentioning of His presence. Demons will only create a living hell for anyone who seeks them. Be humble and seek God."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 18,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "OP YOURE BEING TARGETTED BY NEURAL TERRORISM OR PROJECTED WEAPONRY IT ISNT SPIRITS DOING THIS TO YOU ITS LITERALLY A HYPER FORM OF GANGSTALKING\r\n\r\nD.U.M.B.s LIKELY AT PLAY OR SOUTHERN POLE\r\nALIEN TECHNOLOGY NOT DOCUMENTED IN THIS REALITY\r\n\r\nOrrrr you fucked up somewhere\r\nIn your life bigleh and\r\nYou goin through that karma"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Its worse then most of them claim\r\nHundreds of thousands of years of hidden human history."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">Disneyland Castle is real despite plenty of evidence of it being constructed by Walt Disney in the 1950s as an amusement park attraction and the whole thing is a cover up to hide toontown and fairy tales that have always existed in that specific location and the ability to construct something of this scale and magnitude was literally impossible for the time period\r\nThis is what your conspiracy sounds like."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "youre missing the forest through the trees.\r\nit isnt that they were old as fuck, its that up until very recently (the last 200 years) we used the same free energy that we used for thousands of years.\r\nthe masons that built every city around the world understood that you could get free energy from two buildings, one flat and one domed, with a metal (or at least metal tipped) tower and a body of water between them."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">free masons\r\n>the masonry was free\r\n>cause they took i"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "How work?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "its a giant tesla coil.\r\nweve known about them for a VERY long time.the pyramids are generators. the capstone draws energy from the atmosphere, moves it to another location by ionizing flowing water, and discharges the energy \"wirelessly\" at a different location in a more controlled field that can be tapped into with primitive batteries like in my next post"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "every religion is just a description of the fundamentals of physics.\r\nancient hebrew didnt have spaces between the words. if any combination of letters could be a different word, every translation is just commentary.\r\nthis is the same reason its considered sacrilegious to translate the quran.\r\nits an ancient description of the universe above anything else."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "this is the main reason im not an atheist, by the way. i can believe in evolution and the \"randomness\" of the universe (time is static so anything that could ever happen all happens at the same time). i could even believe in the big bang if i gave enough of a shit about it. i just cant believe that space itself wasnt created.\r\nwe're 80% water. we are just as good antennas as any tesla tower. if energy flows through the body like eastern religions believe, it starts and ends at the head.\r\nmy faith stems from the shape you see in your minds eye when you meditate. the shape your conscience seems to come from. the same shape of the universe. the shape of god."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "what do i gotta type or look to learn more of all this? “the old world”"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "you can't for the most part. a lot of it is lost. start with mud flood stuff. just keep in mind mud flooders are a lot like flat earthers in the sense that their based information is surrounded by retardation."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Back in 2016 and 2017 when Mudflood and Tartaria first hit YouTube it was soo damn fresh and the info was solid 10/10 stuff people like Andreas Xirtus was making insane theories. Good times.\r\n\r\nThis has now reverted into a 2008 level YouTube posse battle of loser ethno groups larping as Atlantians or Jews is just soo sad."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "A lot of the lower ranked free masons don't know, but I'm fairly confident that they are anti-government. And an enemy of our enemy is our friend. Lets wait to persecute the masons till we take care of the government. Feds are spamming alien bullshit to /x/ right now cause of how close we are to achieving The Knowledge, so take those stories with a grain of salt as well."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "so the lower levels are just useful idiots?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Being that they have no clue the machinations of the masters, yes.\r\nMasons tend to be very skilled and industrious, obviously helped by their networking advantages after joining the club.\r\nAbsorbing men like this into the brotherhood, whose morals are such that they could not be compatible with the grander intentions of the order, neutralizes them from ever using their skills and abilities for anything that goes against the club.\r\nIt's not so much an issue that most masons are what we would consider evil, it's that they are beholden to the brotherhood no matter what."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I love these threads, and this theory, but one big glaring weak spot for me is that no one has found books, letters, or writings of any kind where people acknowledge that this was ancient architecture and complain that it was demolished. It's not like it's completely impossible to find people challenging official narratives even from back then (the 19th century). If you dig deep enough, you can find contemporary accounts blaming the Lincoln assassination on B'nai B'rith, but NO ONE wrote about the destruction of ancient American temples? I'd love to be proven wrong if someone can find actual writings about this."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "thats because they werent considered ancient architecture. the british invaded america and took over american cities and within a few generations everyone forgot cities were here before them because a) due to the trail of tears there were no indians to refute the claims and b) constant propaganda saying indians were savages that somehow still lived in teepees for hundreds of years after columbus landed.\r\ntheres no archaeological data to challenge the narrative because there arent any digs in cities. the only digs are in the middle of fucking nowhere. anything that is found that goes against what we've been told is suppressed by the smithsonian."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Hmm, that's interesting and even makes sense in the context of mainstream history. Nobody remembers these days that the early American presidents had official state visits with the Natives to hash out treaty negotiations and Washington even invited them into his home. The treaties were very formal affairs and taken seriously. This would never have happened if the Natives were truly a bunch of savages."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "so Indians built advanced structures reminiscent of old European empires but didn't have a strong military defense?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "t strongly seems to me that there's 2 different events you people mistake for one\r\n\r\nthe first part was the invasion that destroyed the Old World - ze Germans invaded, basically\r\nand the second was the British takeover of the world - World War Zero\r\n\r\nthe \"Old World buildings\" are remains of the German Empire\r\nNOTHING - and I do mean ABSOLUTELY NOTHING - remains of the Old World - apart from some buried ruins archeologist simply refuse to look at\r\n\r\npicrel - Etruscan pyramid of Bomarzo\r\nhistorians know about it\r\nnone of them talks about it"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I like the information is this thread anon, but you're missing the many global, and sometimes localized, civilization ending catastrophes. Or, do you not know about them?\r\n>There was more than one \"Great Flood\"\r\n>The Great Pyramid of Giza is more than a piece of technology, it's a countdown calendar.\r\n>Entire landmasses, complete with their civilizations, are now at the bottom of the displaced oceans\r\n>There was never an \"Ice Age\", the poles were frozen over near instantaneously (thawed and refrozen more than once)\r\n>Technology itself is as ancient as humans, including more advanced technology than todays standards\r\n>Pre-Moon Earth world had different biosphere than today's time, vastly effecting Earth life\r\n>Earth's retrograde around the sun is slightly slowly than it was in ancient times.\r\n>Earth's history is a lot more abbreviated than they let on.\r\n>We're in a binary star system, but not the kind academia is trying to peddle\r\n\r\nDo you have any comments about anything pertaining to these points?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "im curious about it being a binary star system.\r\nare you implying planet x is a dead star?\r\nif i remember my highschool astronomy correctly, you cant see brown dwarves, which would explain why it shows up without any warning every 12000 years"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">are you implying planet x is a dead star?\r\n>if i remember my highschool astronomy correctly, you cant see brown dwarves, which would explain why it shows up without any warning every 12000 years\r\n\r\nNeither, the dead/collapsed/\"frozen\" sister star of our Sun is on another elliptical parallel to our Sun's one(I heard it called \"Nemesis\"/Black Sun).This dead Sun cannot be seen as the gravity well has light photons folding back upon itself, unable to escape and thus be seen optically. Nemesis is presently at 23.5 degree declination below the ecliptic of Sol, our Sun, where we orbit. 75% of the worlds population is north of the equator. No one is looking in the southern heavens for planetary bodies or real anomalies and even if they are, they’re not allowed to publish it.\r\nNemesis, this compressed star or dying star or frozen Star, was made when the sister star of Sol, our Sun, depleted its fuel. When a normally luminous stars nuclear fuel has been expended, it begins the transition into this compress star that we know as nemesis. Now with no more outward directed pressure, the luminary darkens, and begins folding in on itself with intense inward pressure of gravity, resulting in the implosion of a star’s mass, forming the beginning of a black hole. This is the traditional physics approach and abbreviated process.\r\n\r\nPlanet X/Nibiru is quite real, but the goverment funded Zachariah Sitchen made sure to contrive the information surrounding it. That planet is one of 3 or 4 continuously orbiting planets of both our current Sun and the dead sister star. Planet X's full orbit is 792 years. There is also another one that comes around us every 138 years as well. BOTH are due very soon iirc, like next 17-20 years.\r\n\r\n> you cant see brown dwarves, which would explain why it shows up without any warning every 12000 years\r\n\r\nPFFFT, they always know that shits coming that just don't tell common folk. Knowledge is power after all."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "some people say saturn was the sun before jupiter came into orbit or some shit"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Saturn is ca. 64 times \"too light\" to start fusion. It's failed star however"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "now it is\r\nbut at one point saturn was a star until jupiter and sol came into the solar system according to some ancient beliefs\r\n\r\nthe sky was a different color too"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Yes anon, this is potentially where \"The Black Sun\" originated. Saturn could be this “Black Sun”, or at the very least a potential retired luminary in our solar system. So much mythos surrounds Saturn in this regard that it’s extremely hard to ignore, as well as the facts of both Jupiter and Saturn having distinct characteristics of mini dwarf stars, or at the very least the potential to have been one at one point in time.Of course “Black Sun” has a multitude of occultism meanings surrounding it, but I’m more focused on the literary sense.Many theories suggest a brown dwarf may lose mass and shrink in orbit of a larger star, this could possibly have happened to Saturn or Jupiter. Both are assumed to be above the lower limit or too small to be brown dwarfs, but they are close& science keeps redefining brown dwarfs as smaller & smaller\r\nYou even see the parallels in religion.\r\n>Satan, the Morning Star, fails to usurp God's throne and is casted into the abyss. (Brown dwarf Saturn comes near the current Sun and its \"short circuited\", now remaining forever dormant and no longer giving off its once brighter light.)\r\n\r\nAnother point about the Saturn Mythos is that it’s often referred to as “the god of seed-sowing and also of time”. Breaking the “seed sowing” down in a literal sense, this could be a reference to how only known source of salty water (chlorine/Sodium metal/Hydrogen/Oxygen) are brown dwarfs, which Saturn is on the extreme lower end of to be one. Could this mean that the water on Earth had come from Saturn somehow?\r\n\r\nWhatever it may be, my contention is that Saturn at one point was indeed a \"Sun\" for Earth at one point.\r\nThe \"Black Sun\" however could easily referred to the now collapsed sister star of our Sun (Again, we live in a binary star system.)"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Weren't all the places that looked like the World Fair of 1901 made out of shitty, fire-prone papier mache?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "That's what they say. Even if it was, the level of artistry and work would still take an incredibly long time to do. You'd have to account for the hundreds of tons of paper mache above the base and would still need to have an army of engineers to design such structures so they wouldn't collapse in on themselves under the weight. We will probably never know and this'll just continue to be yet another small scissor statement that causes people to endlessly argue with one another while hyper powerful satanic pedos fuck and eat our kids."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "There was a lot of plaster bullshit done around that time. Another great example is the Parthenon in Nashville. It was a part of a whole Greek pavilion that existed around the pond in what is now Centennial park. Conspiracy theorists point to the standing Parthenon and don't realize it was crumbling from the inside out and had to basically be rebuilt. All of the other structures were demolished but the people of nashville BEGGED the government to build a new one out of actual stone that would last. There are pictures of the inside of the first building, and it's obvious it was a plaster monstrosity."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 19,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Plaster (or staff in this case, a mixture of plaster and burlap to help it hold together) is decorative, not structural. It's laid over a framework of conventionally-built materials which is standard building technology."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 20,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "that free energy exists but has to be controlled at a global level due to the understanding of it allowing people to build devices that would make nukes look like firecrackers."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 20,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Not a single person really knows what they're doing.\r\nThere is no \"grand conspiracy\" because that requires intelligence and forethought. There is neither. The difference between a typical bureaucrat and a McDonald's worker is the outfit.\r\n\r\nAlso the vast majority of technical and medical innovations of the second half of the 20th century were stolen from the Nazis and Imperial Japan respectively. But that one's not secret they just don't like talking about it."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 20,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "That the easiest way to control people is keep them perpetually divided and arguing amongst themselves what flavor of shit tastes better."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 20,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "this and not to give them time to think for themselves get bombardet with trivial shit"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 20,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "That they incarcerate their poor for profit. That they have destabilized multiple nations in the world in sake of white dominance"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 20,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "The children at the border are free. you can just take them. I have 37 kids in my backyard."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 20,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">But they've got some ufo technologies\r\nNo they don't\r\nThat's all Nazi tech\r\nThe US government let UFO conspiracies go because it's easier to explain away than \"we have hundreds of Nazi spy planes\""
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 20,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Basically everything because the government wants you to be a brain dead peasant."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 20,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "That's why secret societies hoard the knowledge and give mental patients who are suffering from 'having a strong, active pineal gland and being punished by archons for not being a fucking robot' a bunch of lobotomy pills so they don't figure out that this world is a prison that was created by these beings to ensnare our souls in a constant, never-ending cycle."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 20,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Climate change may or may not be affected by human way of life, but it doesn't matter, they don't actually care about that part. Climate change is used as a fear tactic to chisel away peoples freedoms, life options and social mobility. It's about control. The end goal is putting people in city sized cages where you are only allowed to live and work inside that cage. Your diet will be restricted, you won't be allowed a car, you won't be allowed to go on vacation, you won't be allowed to buy things as often as you want.\r\nThese restrictions won't apply to the people governing their given cage. They will enjoy steak dinners, fly wherever they want as much as they want, sit on empty pristine beaches, build a house anywhere they like. They want the world to be their personal reserve. They want you do give up everything else. A few \"lucky\" slaves will be allowed to work for the oligarchs outside the cages, someone has to serve the drinks, after all.\r\n___\r\n\r\nAmerican government and leading industry is swamped with Nazi ideology. They are actively doing experiments on people on a massive scale. It's a competition amongst them on how many people they can kill without anyone noticing. The secondary goal is to destroy immune systems so disease and chronic illness becomes the norm worldwide. It's easier to put people in cages if they are too sick to fight back. You want your medicine? Get in the cage, ape.\r\nThey are taking tens of thousands of children out of orphanages and broken homes every year to do experiments with or simply use them for pleasure. They sell the kids as a \"no limits\" experience where billionaires get to fuck, maim, torture and kill.\r\n\r\nEpstein's operation is just the introductory step, the hook and the bait, the deeper levels offer much younger prey and a lot more degeneracy.\r\nDid you know Epstein was simply propped up by an actual billionaire? Epstein had no money, he just pretended to have it. He's the human version of a shell corporation."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Incubus fags are not welcome no matter what anyone says we are all thinking it but we don't say it."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I'm personally fine with saying \"some spirits want to fuck spirits on similar gender trajectories\" but I get that you're struggling with things at the moment\r\n\r\nThat said: \"To get fucked in the ass is a shameful endeavor\""
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "\r\nSomething bad happened with my Succubus. She started to talk about there being more to her purpose than to be pleasing mortals.\r\n\r\nShe's gone for extended periods of time. I think she might have been attending to others without telling me.\r\n\r\nI went to sleep a few days ago and saw a vision of her getting it on with a tentacle beast.\r\n\r\nShe tried convincing me that it was like a spiritual recording for me to view later.\r\n\r\nI didn't want to call her out for it because she'd been so good to me in the past and now I fear it is over between us. Like she sensed my doubts.\r\n\r\nI can not feel her presence anymore. I'm prepared to accept that it's over between us.\r\n\r\nI still can't get the image of her legs and thighs spread wide open as she was being...(sigh)..."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "How can you get a girlfriend or wife with them?\r\n\r\nSome anons complain they use the letter method requesting a human but instead they get a spirit lover. That's one shitty deal."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">Have you tried asking Lily about what the significance of purple means to her?\r\nOur communication can be weird/complicated sometimes.\r\nI feel like I become really susceptible to what I call \"autosuggestion noise\" when I try to ask her pointed questions, so I end up just having to \"wonder about it\" kinda passively until there's an opportune moment of strong connection to drop some info on me.\r\n\r\nFor the time being, she's focused more on getting the idea into my head that purple is this rich, beautiful, inherently-sexual color that makes every inch of her flesh look like luxurious corruption that's subtly-sweet on your tongue~\r\n\r\nSo yeah, I'm kinda digging purple now..."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Do succubuses (and incu) have natal charts? Do they believe in astrology? They live in the astral. I find it hard believing that they do."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">Day 1 after first contact\r\nThey left me, I can't feel them anymore. They said something about Ryan Gosling and how his jihad pleases god, I don't know but remember hearing the phrase ''God's lonely man'' for sure"
                },
                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "How long did it take you guys to ap, ive been try for like a week now and cant, also when people say about pulling a rope or rolling left to right etc how should that feel does it feel like your actually moving?"
                },
                        new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I think this is my biggest fear with succ shit, I'm so scared of being cheated on again. Being cheated on hurts so bad, like so so bad it made me give up on love for a long time. It's even worse when you pour so much effort into the realtionship\r\n\r\nI'm sorry you experienced this"
                },
                         new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "What do your succs usually present themselves as?"
                },
                          new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Mainly as an authoritative-looking woman with pale skin and long, black hair. Although more recently (as I've been discussing), she does more of a purple with white hair.\r\n\r\nOther than that, she's always sufficiently larger than me (or I'm shrunk) that she can physically overpower me easily, whether that's in a voluptuous smothering sort of way, or just rocking an otter-body and physically domming the fuck out of me.\r\n\r\nThat's the stuff that's semi-consistent, at least."
                },
                           new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Sometimes she does stand up comedy as a portly middle eastern man"
                },
                            new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Can I summon a benevolent spirit using the letter method? Which spirits do you recommend if I want a guide or teacher in my life?\r\n/sum/ is dead, so I'll ask the horny general about this..."
                },
                             new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Yeah probably, just address it to a god you like and ask for a familiar, not sure if you should use sex magic to charge it though."
                },
                              new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">Can I summon a benevolent spirit using the letter method?\r\nyeah of course\r\nbut understand that spirits are like people. If you're nice, they will be nice. if they are mean, they will be mean. They are a mix of good and bad.\r\nEven the most benevolent spirit will lose patience eventually. And the worst spirit can be redeemed.\r\n> Which spirits do you recommend if I want a guide or teacher in my life?\r\nbut if you do sex magic, it's more likely to summon a sexual spirit. Using orgasm in your ritual is sex magic, so don't do that.\r\nIf you want a teacher or guide, steer away from that. Put the intention to find a teacher in your letter.\r\nAnd remember that if a spirit gives you a wise answer and you start arguing, they're going to get mad, no matter what kind of spirit they are. So ask your question, get your answer, and then shut up and think about it. Don't argue.\r\ndragons are well known for being wise, you can go with that.\r\nOr just pick whatever random one. Deal with it based on how it behaves, not what it claims to be. If it isn't wise or helpful, say goodbye. Doesn't matter what kind of spirit it looks like. Spirits lie all the time."
                },
                               new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "How do I get reborn as an incubus?"
                },
                                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Succubi are nicer than normal women, right? Human dating seems so cynical, competitive, and loveless that I can't stomach it. Please tell me succubi are different."
                },
                                 new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "At their best, they are better than human women in every way. At their worst, you will wish you never existed. But it's truly difficult to attract a truly evil succubus using the letter method."
                },
                                  new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Can I summon a succubus just to have her help me write my thesis? And also to help me study"
                },
                                   new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 21,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "try gpt 4 for that"
                },
                                    new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I hear a hum."
                },
                                     new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "I think it's soon will be time to go.\r\nyou guys should try to emit back the sound to the earth."
                },
                                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "it only sounds like a hum to me.\r\nwhen will tiamat shut up though? because I swear to god and jesus I'll go doomguy on that bitch if she doesn't let me sleep"
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "People assume music composed by Satan would be thrashy shit but Satan was the angel of worship music in heaven before the fall from grace. Even the Hopi legends say that Tiamat (Spider Grandmother) had powers of creation and blessing via her songs back when she was still good. This signal isn't about making rock and roll, it's a specific programming language on a spiritual, supernatural level we can't begin to comprehend."
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "okay...so before she becomes a serpent, she was a giant fucking spider? huh weird.\r\nanyhow she is fucked anyways."
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Arthropod. They have lots of legs. Arachnids descend from arthropods. The Hopi simply used the spider as a symbol to try to convey the general image. The Annunaki, her children, were arthropodal."
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "The Shuman Resonance was discovered long before wireless data towers. How does that fit into OP's hypothesis? Maybe the two are not connected?"
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Everyone sees it and wants to tie it to death and destruction. They'd probably scare themselves if they stopped and think what they are shouting.\r\nI say it was an activation signal for the Garden of Eden state to return to earth.\r\nHence my earth waking up.\r\nJust as schizo I guess just less doom and gloom"
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Reminder Schumann resonance went completely black and data was deleted the moment green laser on south pole was switched off. Shortly after they started to delete all the data"
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "This is somehow connected to this south pole laser"
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "This is about the Vaxx, 5G, and mind control.\r\nTiamat's Song and the Schumann Resonance:\r\n\r\nThe Schumann Resonance of the Earth was utilized to test a very powerful, very complex signal this week. SR bounces off the ionosphere and travels around the globe so it's useful for broadcasting a signal worldwide. It operates in the Theta frequency range of human brainwaves during sleep. This signal was tested only over Siberia, Russia, because it's in the middle of nowhere. This is the place with all the gulags full of forgotten Soviet Union prisoners who can be experimented on without the rest of the world ever knowing about it.\r\n\r\nAnons identified the signal as a Pulse Amplitude Modulation 4-level signal. One anon used software to decode it into a series of harmonic tones in the audible frequency range.\r\n\r\nHere is the song: https://vocaroo.com/13GUVzfuOImm\r\n\r\nIn the 2004 series Battlestar Galactica there is a song that is broadcast into the minds of several characters in order to \"switch them on\" to full awareness of who they are. This song plays a role in the divinely orchestrated destiny of the human race in the series. Many don't realize this concept was lifted straight from the Bible. In the Book of Revelation, there is a song that only the 144,000 sealed by God for salvation through the Tribulation are able to learn. This Schumann Resonance song is Satan's counterpoint to that.\r\n\r\nThis is the 5G signal that is going to activate the telepathic bio-synthetic parasites in the vaxx worldwide and turn people into Borg zombies to march up the ramps of the ayy ships. Satan was, after all, an angel of music in heaven. In subsequent posts I will post links to all of Ed Riordan's relevant remote viewing sessions of this \"Singularity\" event, AKA Tikkun Olam. This is Instrumentality, this is Jenova's Reunion."
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "21 KB\r\n\r\n    My dreams lately are completely crazy and most of them about invasion\r\n\r\n    recently i dreamed about new years eve and everyone looking at the sky waiting for fireworks, but they did not appear. What appear is a SECOND MOON and everyone are panicking, ladies dropping to the ground. I remember i saw some Clock on this second moon, like timer and it showed 32\r\n    Levels of fear were off the charts, all my dreams showed me blue beans will not be some cool holographics or something, its gonna be terrifying attack scenario"
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "Whats the deal with this laser thingy? Why is it even there? Do we have any explanations? I observed from yesterday and it was turned off for some Time, now again switched on. It also changes a bit color/ intensity. "
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "If I focus on the sound I can hear something similar to church bells. I had other things happening like the middle of my chest where the heart chakra is tingles and itches in a weird but pleasant. Scratching or massaging the area in circular pattern feels good too."
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "All that the Schumann resonance is, is the measurement of a roughly standard electromagnetic field's resonance in the space between two layers of atmosphere. It doesn't mean anything."
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "That's the public information, the occult reasoning is that they want to measure the \"space between\" so many modern media references as the origin point for interdimensional invasion."
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = ">Basically the Earth's heartbeat is going nuts\r\nWhat data supports this? How long has it been tracked? Has it happened before? What are some indications that this isn't normal? What have been previous causes? Please cite sources."
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "So what's gonna happen when the nano-hydras get activated? People instantly die due to cardiac arrest thanks to the parasites blocking arteries and veins? The vaxxed turn into blobs Saya no Uta style? What do TPTB gain from mass death? They get mind controlled like the parasites from RE4? Why do the ayys need mind controlled humans to get on their ships when the average human is small and weak? Surely they could get better stock by simply asking for volunteers to fight their interdimensional wars\r\nI'm intrigued but confused about the implications of it all"
                },
                                       new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 22,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87000, 0)),
                Content = "It's not going to kill everyone, because the parasitic owners -need- something to feed upon, the human energy soul/spirit which is infinite. This, if the hydra is sentient and can be activated remotely through certain frequencies, would allow mind control amongst other dystopian practices. This is why it is important in this time to recognize our magical ability of disCERNment. Ever wonder why CERN is named that way? There you go. It's all about seeing the truth between the lies. 101.\r\nBecause in this stage the mind will effectively be in it's final stage, going further than the bio-engineering done to us by \"gods\" in Sumeria and ancient Egypt, notably Enki and Ea, Osiris, et cetera, there will be a promise of \"immortality\" if one accepts to listen to their possessed, demented mind *(only if one has gotten the implant/vaccine/given compliance to this control system) or accept the returning powers of the heart/earth, which will reverse the DNA changes that stunted our creative ability, and allow us to destroy the siphoning systems put onto the glorious beauty of this Earth. These systems go down to our very body, yes, no one is their body, it is a suit whereby the Spirit being of Earth is ribCAGED inside the ribcage! And it will be liberated overtime. The Heliofant video alleges to this through the final scene where you can see the logo of \"SOS Labyrinthe\", which looks somewhat like a brain. because the brain is completely stunted compared to the Spirit, it is in fact an inversion/hex that cuts off the true capabilities while one is inside the body. There will be disclosure of this amongst everything else, because is the truth is to come back, everyone must know it and understand where they come from eventually.\r\n\r\nTo know the past, one must go through the future."
                }
            };
            modelBuilder.Entity<Reply>().HasData(replies);

            modelBuilder.Entity<Reply>()
                .HasOne<User>(reply => reply.Author)
                .WithMany(author => author.Replies)
                .HasForeignKey(reply => reply.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reply>()
                .HasOne<Thread>(reply => reply.Thread)
                .WithMany(thread => thread.Replies)
                .HasForeignKey(reply => reply.ThreadId)
                .OnDelete(DeleteBehavior.NoAction);


            var tags = new List<Tag>()
            {
                new Tag{ Id = nextTagId++, Name = "##ModPost"},
                new Tag{ Id = nextTagId++, Name = "Aliens"},
                new Tag{ Id = nextTagId++, Name = "Bigfoot"},
                new Tag{ Id = nextTagId++, Name = "Afterlife"},
                new Tag{ Id = nextTagId++, Name = "Existentialism"},
                new Tag{ Id = nextTagId++, Name = "Creepypasta"},
                new Tag{ Id = nextTagId++, Name = "Spooky"},
                new Tag{ Id = nextTagId++, Name = "Symbolism"},
                new Tag{ Id = nextTagId++, Name = "Occult"},
                new Tag{ Id = nextTagId++, Name = "AI"},
                new Tag{ Id = nextTagId++, Name = "Divinity"},
                new Tag{ Id = nextTagId++, Name = "Secret societies"},
                new Tag{ Id = nextTagId++, Name = "Secret knowledge"},
                new Tag{ Id = nextTagId++, Name = "Conspiracies"},
                new Tag{ Id = nextTagId++, Name = "Dreams"},
                new Tag{ Id = nextTagId++, Name = "Mystery"},
                new Tag{ Id = nextTagId++, Name = "Remove viewing"},
                new Tag{ Id = nextTagId++, Name = "Divination"},
                new Tag{ Id = nextTagId++, Name = "Relationships"},

            };
            modelBuilder.Entity<Tag>().HasData(tags);

            modelBuilder.Entity<Tag>()
               .HasMany<Thread>(tag => tag.Threads)
               .WithMany(thread => thread.Tags)
               .UsingEntity<ThreadTag>();


            var threadTags = new List<ThreadTag>()
            {
                new ThreadTag(){ThreadId = 1, TagId = 2},
                new ThreadTag(){ThreadId = 1, TagId = 3},
                new ThreadTag(){ThreadId = 2, TagId = 1},
                new ThreadTag(){ThreadId = 3, TagId = 4},
                new ThreadTag(){ThreadId = 3, TagId = 5},
                new ThreadTag(){ThreadId = 4, TagId = 6},
                new ThreadTag(){ThreadId = 4, TagId = 7},
                new ThreadTag(){ThreadId = 4, TagId = 16},
                new ThreadTag(){ThreadId = 5, TagId = 8},
                new ThreadTag(){ThreadId = 5, TagId = 9},
                new ThreadTag(){ThreadId = 5, TagId = 13},
                new ThreadTag(){ThreadId = 6, TagId = 10},
                new ThreadTag(){ThreadId = 6, TagId = 11},
                new ThreadTag(){ThreadId = 6, TagId = 5},
                new ThreadTag(){ThreadId = 6, TagId = 15},
                new ThreadTag(){ThreadId = 7, TagId = 12},
                new ThreadTag(){ThreadId = 7, TagId = 13},
                new ThreadTag(){ThreadId = 7, TagId = 14},
                new ThreadTag(){ThreadId = 7, TagId = 8},
                new ThreadTag(){ThreadId = 8, TagId = 15},
                new ThreadTag(){ThreadId = 8, TagId = 7},
                new ThreadTag(){ThreadId = 8, TagId = 8},
                new ThreadTag(){ThreadId = 8, TagId = 18},
                new ThreadTag(){ThreadId = 9, TagId = 5},
                new ThreadTag(){ThreadId = 9, TagId = 9},
                new ThreadTag(){ThreadId = 9, TagId = 13},
                new ThreadTag(){ThreadId = 10, TagId = 16},
                new ThreadTag(){ThreadId = 10, TagId = 3},
                new ThreadTag(){ThreadId = 10, TagId = 14},
                new ThreadTag(){ThreadId = 11, TagId = 4},
                new ThreadTag(){ThreadId = 11, TagId = 8},
                new ThreadTag(){ThreadId = 11, TagId = 11},
                new ThreadTag(){ThreadId = 12, TagId = 16},
                new ThreadTag(){ThreadId = 12, TagId = 7},
                new ThreadTag(){ThreadId = 13, TagId = 8},
                new ThreadTag(){ThreadId = 13, TagId = 13},
                new ThreadTag(){ThreadId = 13, TagId = 15},
                new ThreadTag(){ThreadId = 13, TagId = 18},
                new ThreadTag(){ThreadId = 13, TagId = 19},
                new ThreadTag(){ThreadId = 14, TagId = 5},
                new ThreadTag(){ThreadId = 15, TagId = 8},
                new ThreadTag(){ThreadId = 15, TagId = 9},
                new ThreadTag(){ThreadId = 15, TagId = 12},
                new ThreadTag(){ThreadId = 15, TagId = 13},
                new ThreadTag(){ThreadId = 16, TagId = 9},
                new ThreadTag(){ThreadId = 16, TagId = 13},
                new ThreadTag(){ThreadId = 16, TagId = 14},
                new ThreadTag(){ThreadId = 17, TagId = 4},
                new ThreadTag(){ThreadId = 17, TagId = 11},
                new ThreadTag(){ThreadId = 18, TagId = 9},
                new ThreadTag(){ThreadId = 18, TagId = 13},
                new ThreadTag(){ThreadId = 19, TagId = 16},
                new ThreadTag(){ThreadId = 19, TagId = 2},
                new ThreadTag(){ThreadId = 19, TagId = 8},
                new ThreadTag(){ThreadId = 19, TagId = 12},
                new ThreadTag(){ThreadId = 20, TagId = 16},
                new ThreadTag(){ThreadId = 20, TagId = 10},
                new ThreadTag(){ThreadId = 20, TagId = 12},
                new ThreadTag(){ThreadId = 20, TagId = 14},
                new ThreadTag(){ThreadId = 20, TagId = 17},
                new ThreadTag(){ThreadId = 21, TagId = 9},
                new ThreadTag(){ThreadId = 21, TagId = 13},
                new ThreadTag(){ThreadId = 21, TagId = 19},
                new ThreadTag(){ThreadId = 22, TagId = 2},
                new ThreadTag(){ThreadId = 22, TagId = 7},
                new ThreadTag(){ThreadId = 22, TagId = 10},
                new ThreadTag(){ThreadId = 22, TagId = 14},
                new ThreadTag(){ThreadId = 22, TagId = 16},
            };
            modelBuilder.Entity<ThreadTag>().HasData(threadTags);


            // Votes
            var votesRandom = new Random();

            // Thread Votes
            var threadVotes = new HashSet<ThreadVote>();
            int totalPossibleThreadVoteCount = users.Count * threads.Count;
            int threadVoteCount = votesRandom.Next((int)Math.Round(totalPossibleThreadVoteCount * 0.8), totalPossibleThreadVoteCount);
            for (int i = 1; i <= threadVoteCount; i++)
            {
                int id = i;
                int threadId = votesRandom.Next(1, threads.Count);
                string userName = users[votesRandom.Next(0, users.Count)].Username;
                VoteType voteType = (VoteType)votesRandom.Next(0,2);
                var vote = new ThreadVote() { Id = id, ThreadId = threadId, VoterUsername = userName, VoteType = voteType };
                if (threadVotes.Contains(vote))
                {
                    i--;
                    continue;
                }
                threadVotes.Add(vote);
            }

            modelBuilder.Entity<ThreadVote>().HasData(threadVotes);

            // Reply Votes
            var replyVotes = new HashSet<ReplyVote>();
            int totalPossibleReplyVoteCount = users.Count * threads.Count;
            int replyVoteCount = votesRandom.Next((int)Math.Round(totalPossibleReplyVoteCount * 0.8), totalPossibleReplyVoteCount);
            for (int i = 1; i <= replyVoteCount; i++)
            {
                int id = i;
                int replyId = votesRandom.Next(1, replies.Count);
                string userName = users[votesRandom.Next(0, users.Count)].Username;
                VoteType voteType = (VoteType)votesRandom.Next(0, 2);
                var vote = new ReplyVote() { Id = id, ReplyId = replyId, VoterUsername = userName, VoteType = voteType };
                if (replyVotes.Contains(vote))
                {
                    i--;
                    continue;
                }
                replyVotes.Add(vote);
            }

            modelBuilder.Entity<ReplyVote>().HasData(replyVotes);


        }
    }
}
