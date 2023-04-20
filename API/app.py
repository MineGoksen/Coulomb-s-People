from flask import Flask, render_template, request, redirect, url_for
from flask_sqlalchemy import SQLAlchemy
from flask.templating import render_template
from flask_migrate import Migrate, migrate
from flask import Flask, jsonify
from collections.abc import MutableMapping
import pyrebase
import firebase_admin
from firebase_admin import credentials
from firebase_admin import db
import random


firebaseConfig = {
    'apiKey': "AIzaSyCOOIrQAej6hFn_C1-0AZjKt_mnbZvHIY8",
    'authDomain': "pentavr-75e47.firebaseapp.com",
    'databaseURL': "https://pentavr-75e47-default-rtdb.europe-west1.firebasedatabase.app",
    'projectId': "pentavr-75e47",
    'storageBucket': "pentavr-75e47.appspot.com",
    'messagingSenderId': "1029655336853",
    'appId': "1:1029655336853:web:63406a2ee162a67b90889c"
}
firebase=pyrebase.initialize_app(firebaseConfig)
auth=firebase.auth()

app = Flask(__name__)
app.debug = False
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///site.db'
db = SQLAlchemy(app)
migrate = Migrate(app, db)

class User(db.Model):
    id = db.Column(db.Integer, primary_key=True, autoincrement=True)
    username = db.Column(db.String(80), unique=True, nullable=False)
    password = db.Column(db.String(120), unique=False, nullable=False)

    def __init__(self, username, password):
        self.username = username
        self.password = password

"""
admin = User("PentaVR", "PentaVR1234*")
db.session.add(admin)
db.session.commit()
"""

class Question(db.Model):
    id = db.Column(db.Integer, primary_key=True, autoincrement=True)
    question = db.Column(db.String(1000), nullable=False)
    choice_a = db.Column(db.String(500), nullable=False)
    choice_b = db.Column(db.String(500), nullable=False)
    choice_c = db.Column(db.String(500), nullable=False)
    choice_d = db.Column(db.String(500), nullable=False)
    country = db.Column(db.String(500), nullable=False)
    right_answer = db.Column(db.String(500), nullable=False)

    def __init__(self, question, choice_a, choice_b, choice_c, choice_d, country, right_answer):
        self.question = question
        self.choice_a = choice_a
        self.choice_b = choice_b
        self.choice_c = choice_c
        self.choice_d = choice_d
        self.country = country
        self.right_answer = right_answer
    def to_api(self):
        return {'id': self.id, 'question': self.question, 'choice_a': self.choice_a, 'choice_b': self.choice_b, 'choice_c': self.choice_c, 'choice_d': self.choice_d, 'country': self.country, 'right_answer': self.right_answer}
    def to_api_with_country(self,country):
        if country==self.country: 
            return {'id': self.id, 'question': self.question, 'choice_a': self.choice_a, 'choice_b': self.choice_b, 'choice_c': self.choice_c, 'choice_d': self.choice_d, 'country': self.country, 'right_answer': self.right_answer}
@app.route('/questionsApi/<country>', methods=['GET'])
def get_question_country(country):
    questions = Question.query.all()
    questions_api = [question.to_api_with_country(country) for question in questions]
    return_list=[]
    for i in range(len(questions_api)):
        if questions_api[i]!=None:
            return_list.append(questions_api[i])
    return jsonify(return_list)
@app.route('/questionsApi', methods=['GET'])
def get_questions():

    questions = Question.query.all()
    questions_api = [question.to_api() for question in questions]
    return jsonify(questions_api)

class Tip(db.Model):
    id = db.Column(db.Integer, primary_key=True, autoincrement=True)
    tip = db.Column(db.String(500), nullable=False )
    country = db.Column(db.String(500), nullable=False)
    def __init__(self, tip, country):
        self.tip = tip
        self.country = country
    def to_api2(self):
        return { 'tip': self.tip, 'country': self.country}
    def to_api2_with_country(self,country):
        if country==self.country:
            return { 'tip': self.tip, 'country': self.country}    

@app.route('/tipApi', methods=['GET'])
def get_tip():
    tip = Tip.query.all()
    tip_api = [tip.to_api2() for tip in tip]
    return jsonify(tip_api)
@app.route('/tipApi/<country>', methods=['GET'])
def get_tip_country(country):
    tip = Tip.query.all()
    tip_api = [tip.to_api2_with_country(country) for tip in tip]
    return_list=[]
    for i in range(len(tip_api)):
        if tip_api[i]!=None:
            return_list.append(tip_api[i])
    return jsonify(return_list)

class players(db.Model):
    id = db.Column(db.String(500), primary_key=True)
    nickname = db.Column(db.String(40),unique=True, nullable=False)
    e_mail = db.Column(db.String(100),unique=True, nullable=False)
    point = db.Column(db.Integer, nullable=False)       

    def __init__(self, id, nickname, e_mail, point):
        self.id = id
        self.nickname = nickname
        self.e_mail = e_mail
        self.point = point
    def to_add_point(self):
        return self.point     
    def to_api_players(self):
        return {'id': self.id, 'nickname': self.nickname, 'e_mail': self.e_mail,'point': self.point}  
    def to_api_playerWithId(self,id):
        if id==self.id:
            return {'id': self.id, 'nickname': self.nickname, 'e_mail': self.e_mail,'point': self.point}  
    def to_api_playerWithemail(self,email):
        if email==self.e_mail:
            return self.id    
@app.route('/playerApi', methods=['GET'])
def get_player():
    player = players.query.all()
    player_api = [player.to_api_players() for player in player]
    return jsonify(player_api)
@app.route('/playerApi/<id>', methods=['GET'])
def get_player_id(id):
    player = players.query.all()
    player_api = [player.to_api_playerWithId(id) for player in player]
    return_list=[]
    for i in range(len(player_api)):
        if player_api[i]!=None:
            return_list.append(player_api[i])
    return jsonify(return_list)           
@app.route('/')
def index():    
    return render_template('login.html')

class codeWithId(db.Model):
    id = db.Column(db.String(500), nullable=False)
    code = db.Column(db.String(6), primary_key=True, nullable=False)
    def __init__(self, id, code,):
        self.id = id
        self.code = code

    def to_api(self,code):
        if code==self.code:
            return {'id': self.id, 'code': self.code}
@app.route('/code/<id>', methods=['GET'])
def get_code(id):
    code = codeWithId.query.all()
    questions_api = [code.to_api(id) for code in code]
    return_list=[]
    for i in range(len(questions_api)):
        if questions_api[i]!=None:
            return_list.append(questions_api[i])
    return jsonify(return_list)

class video(db.Model):
    country = db.Column(db.String(50), nullable=False)
    link = db.Column(db.String(1000), primary_key=True, nullable=False)
    lat = db.Column(db.String(50), nullable=False)
    lon = db.Column(db.String(50), nullable=False)

    def __init__(self, country, link,lat,lon):
        self.country = country
        self.link = link
        self.lat = lat
        self.lon = lon
    def to_firstList(self):            
            return self.country
    def to_returnList(self):
        return {'country': self.country, 'link': self.link,'lat': self.lat,'lon': self.lon}
@app.route('/get_video', methods=['GET'])
def get_video():
    code = video.query.all()
    questions_api = [code.to_firstList() for code in code] 
    random.shuffle(questions_api)
    list=[]
    for i in questions_api:        
        if i not in list:
            list.append(i)   
        if len(list)==4:
            break    
    videos=["","","","",""]
    for i in range(len(list)):
        value=video.query.filter_by(country=list[i]).all() 
        returner=[value.to_returnList() for value in value]
        videos[i]=returner[0]      
    return jsonify(videos)

@app.route('/login', methods=['POST'])
def login():
    username = request.form['username']
    password = request.form['password']    
    user = User.query.filter_by(username=username, password=password).first()
    if user:       
        e_mail= username+"@gmail.com"   
        try:
            auth.sign_in_with_email_and_password(e_mail, password)
        except:
            return render_template('login.html',message="Username or Password is incorrect")
        return redirect(url_for('home'))
    else:
        return render_template('login.html',message="Username or Password is incorrect")

@app.route('/home')
def home():
    users=""+"@gmail.com"
    try:            
            if auth.current_user['localId']=="AjGsYxBK6UbUn4MtsiWu6QrPplG3":
                return render_template('add_question.html')
            else:
                return render_template('login.html',message="You have to login")           
                       
    except:             
            return render_template('login.html',message="You have to login")
@app.route('/add_question', methods=['POST'])
def add_question():
    question = request.form['question']
    choice_a = request.form['choice_a']
    choice_b = request.form['choice_b']
    choice_c = request.form['choice_c']
    choice_d = request.form['choice_d']
    country = request.form['country']
    right_answer = request.form['right_answer']
    new_question = Question(question, choice_a, choice_b, choice_c, choice_d, country, right_answer)
    db.session.add(new_question)
    db.session.commit()
    return redirect(url_for('home'))


@app.route('/add_tip', methods=['POST'])
def add_tip():
    country = request.form['country']
    tip = request.form['tip']
    new_question = Tip(tip, country)
    db.session.add(new_question)
    db.session.commit()
    return redirect(url_for('home'))
@app.route('/add_video', methods=['POST'])
def add_video():
    country = request.form['country']
    link = request.form['link']
    lan = request.form['lan']
    lon = request.form['lon']
    new_video = video(country,link,lan,lon)
    db.session.add(new_video)
    db.session.commit()
    return redirect(url_for('home'))
@app.route('/sign_in_page' ,methods=['GET', 'POST'])
def sign_in_page():  
    if(request.method == "GET"):  
        return render_template('sign_in.html')
@app.route('/sign_up_page' ,methods=['GET', 'POST'])
def sign_up_page():    
    if(request.method == "GET"):
        return render_template('sign_up.html')
@app.route('/add_Point/<id>/<point>', methods=['PUT'])
def add_point(id,point):
        player = players.query.filter_by(id=id).first()
        if player !=None:
            print(player)
            addPoint=int(point)+player.to_add_point()
            updated = players.query.filter_by(id=id).update(dict(point=addPoint))
            db.session.commit()
        return render_template('sign_up.html')
@app.route('/sign_in', methods=['GET', 'POST'])
def sign_in():     
    if(request.method == "POST"):
        e_mail=request.form.get("e-mail") 
        password=request.form.get("password") 
        code=request.form.get("code")
        try:            
            login = auth.sign_in_with_email_and_password(e_mail, password)
            token=None             
            try:         
                token = login['localId']                
            except:
                return render_template('sign_in.html',message="Invalid token")  
            new_question = codeWithId(token, code)
            db.session.add(new_question)
            db.session.commit()           
            return render_template('sign_in.html',message="Successfully logged in!")
        except:  

            return render_template('sign_in.html',message="Invalid email or password") 
    if(request.method == "GET"):
        return render_template('sign_in.html')
    return render_template('sign_in.html')    
@app.route('/sign_up' ,methods=['GET', 'POST'])
def sign_up():    
    if(request.method == "POST"):
        nickName=request.form.get("nickName")
        data=db.session.query(players.id).filter(players.nickname==nickName).first()  
        if data is not None:            
            return render_template('sign_up.html',message="NickName already exists") 
        e_mail=request.form.get("e-mail")  
        data=db.session.query(players.id).filter(players.e_mail==e_mail).first()  
        if data is not None:            
            return render_template('sign_up.html',message="E-mail already exists") 
        
        password=request.form.get("password") 
        try:
            user = auth.create_user_with_email_and_password(e_mail, password)
            token=None             
            try:         
                token = user['localId']                
            except:
                return render_template('sign_up.html',message="Invalid token")
            new_question = players(token,nickName,e_mail,0)
            db.session.add(new_question)
            db.session.commit()
            return render_template('sign_in.html',message="account created")           
        except:             
            return render_template('sign_up.html',message="Informations already exists") 
    if(request.method == "GET"):
        return render_template('sign_up.html')
    return render_template('sign_in.html')  


if __name__ == '__main__':
    app.run()